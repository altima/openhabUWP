using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using openhabUWP.Models;

namespace openhabUWP.Remote.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPushClientService
    {
        bool PushChannelAttached { get; }

        void AttachToEvents(string baseUrl, string[] topics = null, Action<string> onDataReceived = null, Action<string> onEventReceived = null);
        void PoolForEvent(string url, Action<string> onDataReceived = null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Remote.Services.IPushClientService" />
    public class PushClientService : IPushClientService
    {
        private const string DataPrefix = "data: ";
        private const string EventPrefix = "event: ";
        private bool _dataWillFollow = false;
        private bool _pushChannelAttached;

        public bool PushChannelAttached
        {
            get { return _pushChannelAttached; }
        }

        /// <summary>
        /// Attaches to events (by http long pooling, simple SSE :) )
        /// by http://danielwertheim.se/2013/09/15/using-c-and-httpclient-to-consume-continuously-streamed-results/
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="topics">The topics [default: [smarthome,items,*,state]]</param>
        /// <param name="onDataReceived">The on data received.</param>
        /// <param name="onEventReceived">The on event received.</param>
        /// <returns></returns>
        public async void AttachToEvents(string baseUrl, string[] topics = null, Action<string> onDataReceived = null,
            Action<string> onEventReceived = null)
        {
            if (PushChannelAttached) return;

            //defaults
            if (onDataReceived == null) onDataReceived = (input) => { };
            if (onEventReceived == null) onEventReceived = (input) => { };
            if (topics == null) topics = new string[] { "smarthome", "items", "*", "state" };

            //build uri
            string url = string.Concat(baseUrl, "/events?topics=", string.Join("/", topics));

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Cache-Control", "no-cache");

            using (var client = new HttpClient() { Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite) })
            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                using (var body = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(body))
                {
                    while (!reader.EndOfStream)
                    {
                        _pushChannelAttached = true;
                        var line = reader.ReadLine();
                        if (line.StartsWith(EventPrefix) && !_dataWillFollow)
                        {
                            _dataWillFollow = true;
                            var @event = line.Substring(EventPrefix.Length);
                            onEventReceived.Invoke(@event);
                        }

                        if (line.StartsWith(DataPrefix) && _dataWillFollow)
                        {
                            var data = line.Substring(DataPrefix.Length);
                            onDataReceived.Invoke(data);
                            _dataWillFollow = false;
                        }
                    }
                }
            }
            _pushChannelAttached = false;
        }

        private string xHeader = "";
        public async void PoolForEvent(string url, Action<string> onDataReceived)
        {
            if (PushChannelAttached) return;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("X-Atmosphere-Transport", "long-polling");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Cache-Control", "no-cache");
            //if (!xHeader.IsNullOrEmpty()) request.Headers.Add("X-Atmosphere-tracking-id", xHeader);

            try
            {
                _pushChannelAttached = true;
                using (var client = new HttpClient() {Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite)})
                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    if (response.Headers.Contains("X-Atmosphere-tracking-id"))
                    {
                        xHeader = response.Headers.GetValues("X-Atmosphere-tracking-id").FirstOrDefault();
                    }
                    using (var body = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(body))
                    {
                        var line = reader.ReadToEnd();
                        onDataReceived?.Invoke(line);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                _pushChannelAttached = false;
            }
        }
    }
}
