using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace openhabUWP.Services
{
    public interface IPushClientService
    {
        bool PushChannelAttached { get; }

        void AttachToEvents(string baseUrl, string[] topyics = null, Action<string> onDataReceived = null, Action<string> onEventReceived = null);
    }

    public class PushClientService : IPushClientService
    {
        private const string DataPrefix = "data: ";
        private const string EventPrefix = "event: ";
        private bool _dataWillFollow = false;
        private bool _pushChannelAttached;

        public bool PushChannelAttached { get { return _pushChannelAttached; } }

        /// <summary>
        /// Attaches to events (by http long pooling, simple SSE :) )
        /// by http://danielwertheim.se/2013/09/15/using-c-and-httpclient-to-consume-continuously-streamed-results/
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="topcis">The topcis [default: [smarthome,items,*,state]]</param>
        /// <param name="onDataReceived">The on data received.</param>
        /// <param name="onEventReceived">The on event received.</param>
        /// <returns></returns>
        public async void AttachToEvents(string baseUrl, string[] topcis = null, Action<string> onDataReceived = null, Action<string> onEventReceived = null)
        {
            if (PushChannelAttached) return;

            //defaults
            if (onDataReceived == null) onDataReceived = (input) => { };
            if (onEventReceived == null) onEventReceived = (input) => { };
            if (topcis == null) topcis = new string[] { "smarthome", "items", "*", "state" };

            //build uri
            string url = string.Concat(baseUrl, "/events?topics=", string.Join("/", topcis));
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                var request = new HttpRequestMessage(HttpMethod.Get, url);
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
            }
            _pushChannelAttached = false;
        }
    }
}
