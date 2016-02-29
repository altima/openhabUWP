using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using openhabUWP.Helper;
using openhabUWP.Models;

namespace openhabUWP.Remote.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPushClientService
    {
        bool IsOpenhab1 { get; }

        void StartPolling(string url, string fallbackUrl, Action<string> onDataReceived = null, string[] topics = null);
        void StopPolling();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Remote.Services.IPushClientService" />
    public class PushClientService : IPushClientService
    {
        private IHttpService _httpService;
        private CancellationTokenSource _bCts;
        private Task _pollTask;

        public bool IsOpenhab1 { get; private set; }

        public PushClientService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public void StartPolling(string url, string fallbackUrl, Action<string> onDataReceived = null, string[] topics = null)
        {
            if (_bCts != null)
            {
                _bCts.Cancel();
                _bCts.Dispose();
                _bCts = null;
            }

            _pollTask?.Wait(1);
            _pollTask = new Task(() => StartPollingAsync(url, fallbackUrl, onDataReceived, topics));
            _pollTask.Start();
        }

        private async void StartPollingAsync(string url, string fallbackUrl, Action<string> onDataReceived = null, string[] topics = null)
        {
            try
            {
                string trackkingId = "";
                var client = _httpService.GetClient();
                if (topics == null) topics = new string[] { "smarthome", "items", "*", "state" };

                var pingUrl = string.Concat(url, "/rest/events");

                var openhab1EventUrl = fallbackUrl;
                var openhab2EventUrl = string.Concat(url, "/rest/events?topics=", string.Join("/", topics));

                //check openhab2
                var pingRequest = new HttpRequestMessage(HttpMethod.Head, pingUrl);
                var pingResult = await client.SendAsync(pingRequest, HttpCompletionOption.ResponseHeadersRead);

                if (pingResult.StatusCode == HttpStatusCode.RequestTimeout)
                {
                    return;
                }

                if (pingResult.StatusCode == HttpStatusCode.NotFound)
                {
                    IsOpenhab1 = true;
                    IEnumerable<string> serverVersions;
                    IEnumerable<string> xHeaders;
                    if (pingResult.Headers.TryGetValues("Server", out serverVersions))
                    {

                    }
                    if (pingResult.Headers.TryGetValues("X-Atmosphere-tracking-id", out xHeaders))
                    {
                        trackkingId = xHeaders.First();
                    }
                }

                if (pingResult.StatusCode == HttpStatusCode.OK)
                {
                    IsOpenhab1 = false;
                    trackkingId = string.Empty;
                }

                if (IsOpenhab1) return;

                HttpRequestMessage request;
                if (IsOpenhab1)
                {
                    request = new HttpRequestMessage(HttpMethod.Get, openhab1EventUrl);
                }
                else
                {
                    request = new HttpRequestMessage(HttpMethod.Get, openhab2EventUrl);
                }

                request.Headers.Add("Cache-Control", "no-cache");
                if (IsOpenhab1)
                {
                    request.Headers.Add("Accept", "application/json");
                    request.Headers.Add("X-Atmosphere-Transport", "long-polling");
                    //request.Headers.Add("X-Atmosphere-Transport", "streaming");
                    if (!trackkingId.IsNullOrEmpty()) request.Headers.Add("X-Atmosphere-tracking-id", trackkingId);
                }
                else
                {
                    request.Headers.Add("Accept", "text/event-stream");
                }

                var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(Timeout.Infinite));

                using (
                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cts.Token))
                {
                    using (var body = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(body))
                    {
                        while (!reader.EndOfStream)
                        {
                            if (_bCts.IsCancellationRequested)
                            {
                                _bCts.Token.ThrowIfCancellationRequested();
                                break;
                            }
                            var line = reader.ReadLine();
                            if (IsOpenhab1)
                            {
                                onDataReceived?.Invoke(line);
                            }
                            else
                            {
                                var prefix = "data: ";
                                if (line.StartsWith(prefix))
                                {
                                    var data = line.Substring(prefix.Length);
                                    onDataReceived?.Invoke(data);
                                }
                            }
                        }
                    }
                }


                if (IsOpenhab1 && _bCts != null && !_bCts.IsCancellationRequested)
                {
                    StartPollingAsync(url, fallbackUrl, onDataReceived, topics);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
            }
        }

        public void StopPolling()
        {
            if (_bCts == null) return;
            _bCts.Cancel();
            _bCts.Dispose();
            _bCts = null;
        }
    }
}
