using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using openhabUWP.Enums;
using openhabUWP.Helper;
using openhabUWP.Models;
using Zeroconf;

namespace openhabUWP.Services
{
    public interface IRestService20
    {
        Task<Server[]> FindLocalServersAsync();
        Task<Sitemap[]> LoadSitemapsAsync(Server server);
        Task<Sitemap> LoadSitemapDetailsAsync(Sitemap sitemap);

        Task AttachToEvents(string baseUrl, string[] topyics = null, Action<string> onDataReceived = null, Action<string> onEventReceived = null);
    }

    public class RestService20 : IRestService20
    {
        private HttpClient _client;

        private readonly string[] mDnsProtocols = new string[]
        {
            "_openhab-server._tcp.local.",
            "_openhab-server-ssl._tcp.local."
        };

        private HttpClient Client()
        {
            if (_client == null)
            {
                var clientHandler = new HttpClientHandler();
                if (clientHandler.SupportsAutomaticDecompression)
                {
                    //clientHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                }
                _client = new HttpClient(clientHandler);
            }
            return _client;
        }

        private async Task<string> Get(string url)
        {
            var client = Client();
            var response = await client.GetStringAsync(new Uri(url));
            return response;
        }

        public async Task<Server[]> FindLocalServersAsync()
        {
            var serverList = new List<Server>();

            //todo integrate zeroconf for find openhab servers on network

            var scanTime = TimeSpan.FromSeconds(5);
            var retries = 2;
            var retryDelay = 2000;

            var httpResult = await ZeroconfResolver.ResolveAsync(
                protocols: mDnsProtocols,
                scanTime: scanTime,
                retries: retries,
                retryDelayMilliseconds: retryDelay,
                callback: (x) => Debug.WriteLine(x.DisplayName));

            serverList.AddRange(httpResult.Select(s => new Server(ProtocolType.Http, s.IPAddress)));

#if DEBUG
            serverList.Add(new Server(ProtocolType.Http, "192.168.178.107"));
#endif

            return serverList.ToArray();
        }

        public async Task<Sitemap[]> LoadSitemapsAsync(Server server)
        {
            var sitemaps = string.Concat(server.Link, "/sitemaps");
            return (await Get(sitemaps)).ToSitemaps();
        }

        public Task<Models.Items> LoadItemsAsync(Openhab openhab)
        {
            throw new NotImplementedException();
        }

        public async Task<Sitemap> LoadSitemapDetailsAsync(Sitemap sitemap)
        {
            return (await Get(sitemap.Link)).ToSitemap(sitemap);
        }



        private const string DataPrefix = "data: ";
        private const string EventPrefix = "event: ";
        private bool _dataWillFollow = false;


        /// <summary>
        /// Attaches to events (by http long pooling, simple SSE :) )
        /// by http://danielwertheim.se/2013/09/15/using-c-and-httpclient-to-consume-continuously-streamed-results/
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="topcis">The topcis [default: [smarthome,items,*,state]]</param>
        /// <param name="onDataReceived">The on data received.</param>
        /// <param name="onEventReceived">The on event received.</param>
        /// <returns></returns>
        public async Task AttachToEvents(string baseUrl, string[] topcis = null, Action<string> onDataReceived = null, Action<string> onEventReceived = null)
        {
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
        }

        public Task PostCommand(string url, string command)
        {
            throw new NotImplementedException();
        }
    }
}