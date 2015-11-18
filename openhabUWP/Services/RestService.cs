using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using openhabUWP.Enums;
using openhabUWP.Helper;
using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Services;
using openhabUWP.Models;

namespace openhabUWP.Services
{
    public class RestService : IRestService, IDisposable
    {
        #region HttpClient
        private HttpClient _client;
        private readonly string[] mDnsProtocols = new string[]
        {
            "_openhab-server._tcp.local.",
            "_openhab-server-ssl._tcp.local."
        };
        private HttpClient GetClient()
        {
            if (_client == null)
            {
                var handler = new HttpClientHandler();
                if (handler.SupportsAutomaticDecompression)
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.GZip;
                }
                _client = new HttpClient(handler);
            }
            return _client;
        }
        #endregion

        private IParserService _parserService;
        private ILogService _logService;

        public RestService(IParserService parserService, ILogService logService)
        {
            _parserService = parserService;
            _logService = logService;
        }

        /// <summary>
        /// Finds the local servers asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<Server[]> FindLocalServersAsync()
        {
            var serverList = new List<Server>();

            //todo integrate zeroconf for find openhab servers on network

            //var scanTime = TimeSpan.FromSeconds(5);
            //var retries = 2;
            //var retryDelay = 2000;

            //var httpResult = await ZeroconfResolver.ResolveAsync(
            //    protocols: mDnsProtocols,
            //    scanTime: scanTime,
            //    retries: retries,
            //    retryDelayMilliseconds: retryDelay,
            //    callback: (x) => Debug.WriteLine(x.DisplayName));

            //serverList.AddRange(httpResult.Select(s => new Server(ProtocolType.Http, s.IPAddress)));

            serverList.Add(new Server(ProtocolType.Http, "192.168.178.3"));
            serverList.Add(new Server(ProtocolType.Http, "192.168.178.10"));

            return serverList.ToArray();
        }

        /// <summary>
        /// Loads the openhab links asynchronous.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns></returns>
        public async Task<Openhab> LoadOpenhabLinksAsync(Server server)
        {
            var xml = await GetResponse(server.Link);
            if (!xml.IsNullOrEmpty())
            {
                var xDoc = XDocument.Parse(xml);
                return _parserService.ParseOpenhab(xDoc.Root);
            }
            return new Openhab();
        }

        /// <summary>
        /// Loads the sitemaps asynchronous.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<Sitemaps> LoadSitemapsAsync(Openhab openhab)
        {
            var sitemapUrl = openhab.Links.First(l => Equals(l.Type, "sitemaps")).Value;
            var xml = await GetResponse(sitemapUrl);
            if (!xml.IsNullOrEmpty())
            {
                var xDoc = XDocument.Parse(xml);
                return _parserService.ParseSitempas(xDoc.Root);
            }
            return new Sitemaps();
        }

        /// <summary>
        /// Loads the items asynchronous.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<Models.Items> LoadItemsAsync(Openhab openhab)
        {
            var sitemapUrl = openhab.Links.First(l => Equals(l.Type, "items")).Value;
            var xml = await GetResponse(sitemapUrl);
            if (!xml.IsNullOrEmpty())
            {
                var xDoc = XDocument.Parse(xml);
                return _parserService.ParseItems(xDoc.Root);
            }
            return new Models.Items();
        }

        /// <summary>
        /// Loads the sitemap details asynchronous.
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<Sitemap> LoadSitemapDetailsAsync(Sitemap sitemap)
        {
            var sitemapUrl = sitemap.Link;
            var xml = await GetResponse(sitemapUrl);
            if (!xml.IsNullOrEmpty())
            {
                var xDoc = XDocument.Parse(xml);
                return _parserService.ParseSitemap(xDoc.Root);
            }
            return new Sitemap();
        }


        private async Task<string> GetResponse(string url)
        {
            try
            {
                var client = GetClient();
                return await client.GetStringAsync(url);
            }
            catch { }
            return string.Empty;
        }

        private bool _isDisposed;
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (_client != null)
                {
                    _client.CancelPendingRequests();
                    _client.Dispose();
                    _client = null;
                }
            }
        }
    }
}
