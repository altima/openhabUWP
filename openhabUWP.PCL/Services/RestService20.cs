using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Networking.Sockets;
using openhabUWP.Enums;
using openhabUWP.Helper;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Services;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Models;
using Zeroconf;

namespace openhabUWP.Services
{
    public interface IRestService20
    {
        Task<Server[]> FindLocalServersAsync();
        Task<Sitemap[]> LoadSitemapsAsync(Server server);
        Task<Sitemap> LoadSitemapDetailsAsync(Sitemap sitemap);
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
                    clientHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
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

        public Task PostCommand(string url, string command)
        {
            throw new NotImplementedException();
        }
    }
}