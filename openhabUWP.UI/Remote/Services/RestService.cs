using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using openhabUWP.Enums;
using openhabUWP.Helper;
using openhabUWP.Models;
using openhabUWP.Remote.Models;
using Zeroconf;

namespace openhabUWP.Remote.Services
{
    public interface IRestService
    {
        /// <summary>
        /// Gets the user agent.
        /// </summary>
        /// <value>
        /// The user agent.
        /// </value>
        string UserAgent { get; }

        /// <summary>
        /// Finds the local servers asynchronous.
        /// </summary>
        /// <returns cref="Server"></returns>
        Task<Server[]> FindLocalServersAsync();

        /// <summary>
        /// Loads the sitemaps asynchronous.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns></returns>
        Task<Sitemap[]> LoadSitemapsAsync(Server server);

        /// <summary>
        /// Loads the sitemap details asynchronous.
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <returns cref="Sitemap"></returns>
        Task<Sitemap> LoadSitemapAsync(Sitemap sitemap);

        /// <summary>
        /// Loads the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        Task<Page> LoadPageAsync(Page page);

        /// <summary>
        /// Determines whether the specified server is openhab2.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns></returns>
        Task<bool> IsOpenhab2(Server server);

        /// <summary>
        /// Posts the command.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        Task PostCommand(string url, string command);

        /// <summary>
        /// Posts the command.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        Task PostCommand(Item item, string command);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Services.IRestService20" />
    public class RestService : IRestService
    {
        private HttpClient _client;

        /// <summary>
        /// Gets the user agent.
        /// </summary>
        /// <value>
        /// The user agent.
        /// </value>
        public string UserAgent { get { return "openhabUWP/0.1"; } }

        private string X_Atmosphere_tracking_id = "";

        /// <summary>
        /// The supported mDNS protocols
        /// </summary>
        private readonly string[] _mDnsProtocols = {
            "_openhab-server._tcp.local.",
            "_openhab-server-ssl._tcp.local."
        };

        private HttpClient Client()
        {
            if (_client != null)
            {
                if (!X_Atmosphere_tracking_id.IsNullOrEmpty() && !
                    _client.DefaultRequestHeaders.Contains("X-Atmosphere-tracking-id"))
                {
                    _client.DefaultRequestHeaders.Add("X-Atmosphere-tracking-id", X_Atmosphere_tracking_id);
                }
                return _client;
            }

            var clientHandler = new HttpClientHandler();
            if (clientHandler.SupportsAutomaticDecompression)
            {
                clientHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            }
            _client = new HttpClient(clientHandler);
            //set default headers
            _client.DefaultRequestHeaders.Add("UserAgent", UserAgent);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            return _client;
        }

        /// <summary>
        /// Gets the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private async Task<string> Get(string url)
        {
            var response = await Client().GetAsync(new Uri(url));
            CheckResponseHeaders(response.Headers);
            return await response.Content.ReadAsStringAsync();
        }

        private void CheckResponseHeaders(HttpResponseHeaders headers)
        {
            var xHeader = "";
            if (headers.Contains("X-Atmosphere-tracking-id"))
            {
                xHeader = headers.GetValues("X-Atmosphere-tracking-id").FirstOrDefault();
            }

            if (!Equals(xHeader, X_Atmosphere_tracking_id))
            {
                X_Atmosphere_tracking_id = xHeader;
            }
        }

        private async Task<bool> Post(string url, string body)
        {
            var response = await Client().PostAsync(new Uri(url), new StringContent(body, Encoding.UTF8));
            CheckResponseHeaders(response.Headers);
            try
            {
                return response.IsSuccessStatusCode;
            }
            catch { }

            return false;
        }

        /// <summary>
        /// Finds the local servers asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<Server[]> FindLocalServersAsync()
        {
            var serverList = new List<Server>();

            //todo integrate zeroconf for find openhab servers on network

            var scanTime = TimeSpan.FromSeconds(5);
            var retries = 2;
            var retryDelay = 2000;

            var httpResult = await ZeroconfResolver.ResolveAsync(
                protocols: _mDnsProtocols,
                scanTime: scanTime,
                retries: retries,
                retryDelayMilliseconds: retryDelay,
                callback: (x) => Debug.WriteLine(x.DisplayName));


            if (httpResult.Any(s => Equals(s.IPAddress, "192.168.178.107")))
            {
                //serverList.Insert(0, new Server(ProtocolType.Http, "192.168.178.3"));
            }

            serverList.AddRange(httpResult.Select(s => new Server(ProtocolType.Http, s.IPAddress)));

            if (!serverList.Any())
            {
                serverList.Add(new Server(ProtocolType.Http, "192.168.178.107"));
            }

            return serverList.ToArray();
        }

        /// <summary>
        /// Loads the sitemaps asynchronous.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns></returns>
        public async Task<Sitemap[]> LoadSitemapsAsync(Server server)
        {
            var sitemaps = string.Concat(server.Link, "/sitemaps");
            return (await Get(sitemaps)).ToSitemaps();
        }

        /// <summary>
        /// Loads the sitemap detail asynchronous.
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <returns></returns>
        public async Task<Sitemap> LoadSitemapAsync(Sitemap sitemap)
        {
            return (await Get(sitemap.Link)).ToSitemap(sitemap);
        }

        /// <summary>
        /// Loads the page asynchronous.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public async Task<Page> LoadPageAsync(Page page)
        {
            return (await Get(page.Link)).ToPage(page);
        }

        /// <summary>
        /// Determines whether the specified server is openhab2.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns></returns>
        public async Task<bool> IsOpenhab2(Server server)
        {
            var url = string.Concat(server.Link, "/events");

            using (var client = new HttpClient())
            {
                var result = await client.SendAsync(
                    new HttpRequestMessage(HttpMethod.Get, url),
                    HttpCompletionOption.ResponseHeadersRead);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Posts the command.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task PostCommand(string url, string command)
        {
            //if (!url.EndsWith("/state")) url = string.Concat(url, "/state");
            await Post(url, command);
        }

        public Task PostCommand(Item item, string command)
        {
            return PostCommand(item.Link, command);
        }
    }
}