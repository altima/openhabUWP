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

        string Authentication { get; }

        /// <summary>
        /// Finds the local servers asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<string[]> FindLocalServersAsync();

        /// <summary>
        /// Pings the specified server URL.
        /// </summary>
        /// <param name="serverUrl">The server URL.</param>
        /// <returns></returns>
        Task<bool> Ping(string serverUrl);

        /// <summary>
        /// Loads the sitemaps asynchronous.
        /// </summary>
        /// <param name="serverUrl">The server URL.</param>
        /// <returns></returns>
        Task<Sitemap[]> LoadSitemapsAsync(string serverUrl);

        /// <summary>
        /// Loads the sitemap asynchronous.
        /// </summary>
        /// <param name="serverUrl">The server URL.</param>
        /// <param name="sitemapName">Name of the sitemap.</param>
        /// <returns></returns>
        Task<Sitemap> LoadSitemapAsync(string serverUrl, string sitemapName);

        /// <summary>
        /// Loads the page asynchronous.
        /// </summary>
        /// <param name="serverUrl">The server URL.</param>
        /// <param name="sitemapName">Name of the sitemap.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <returns></returns>
        Task<Page> LoadPageAsync(string serverUrl, string sitemapName, string pageId);

        /// <summary>
        /// Determines whether the specified server URL is openhab2.
        /// </summary>
        /// <param name="serverUrl">The server URL.</param>
        /// <returns></returns>
        Task<bool> IsOpenhab2(string serverUrl);

        /// <summary>
        /// Posts the command.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        Task PostCommand(string url, string command);

        void SetAuthentication(string username, string password);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Services.IRestService20" />
    public class RestService : IRestService
    {
        private Dictionary<string, string> Api = new Dictionary<string, string>()
        {
            {"rest", "{0}/rest"},
            {"sitemaps", "{0}/rest/sitemaps"},
            {"sitemap", "{0}/rest/sitemaps/{1}"},
            {"page", "{0}/rest/sitemaps/{1}/{2}"},

            {"items", "{0}/rest/items"},

            {"events", "{0}/rest/events"},
        };

        private HttpClient _client;

        /// <summary>
        /// Gets the user agent.
        /// </summary>
        /// <value>
        /// The user agent.
        /// </value>
        public string UserAgent { get { return "openhabUWP/10.0"; } }

        public string Authentication { get; private set; }

        private string X_Atmosphere_tracking_id = "";

        /// <summary>
        /// The supported mDNS protocols
        /// </summary>
        private readonly string[] _mDnsProtocols = {
            "_openhab-server._tcp.local.",
            "_openhab-server-ssl._tcp.local."
        };

        public RestService()
        {
            SetAuthentication("", "");
        }

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
            _client.DefaultRequestHeaders.Add("Authentication", Authentication);
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
        public async Task<string[]> FindLocalServersAsync()
        {
            var serverList = new List<string>();

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

            //todo

            foreach (IZeroconfHost host in httpResult)
            {
                var ip = host.IPAddress;
                foreach (var service in host.Services.Where(service => _mDnsProtocols.Contains(service.Key)))
                {
                    var protocol = service.Key.Contains("ssl") ? "https://" : "http://";
                    var port = service.Value.Port;
                    serverList.Add(string.Concat(protocol, ip, ":", port));
                }
            }
            return serverList.ToArray();
        }

        /// <summary>
        /// Pings the specified server URL.
        /// </summary>
        /// <param name="serverUrl">The server URL.</param>
        /// <returns></returns>
        public async Task<bool> Ping(string serverUrl)
        {
            try
            {
                var client = Client();
                var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, serverUrl));
                return result.IsSuccessStatusCode;
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Loads the sitemaps asynchronous.
        /// </summary>
        /// <param name="serverUrl">The server URL.</param>
        /// <returns></returns>
        public async Task<Sitemap[]> LoadSitemapsAsync(string serverUrl)
        {
            var url = string.Format(Api["sitemaps"], serverUrl);
            return (await Get(url)).ToSitemaps();
        }

        /// <summary>
        /// Loads the sitemap asynchronous.
        /// </summary>
        /// <param name="serverUrl">The server URL.</param>
        /// <param name="sitemapName">Name of the sitemap.</param>
        /// <returns></returns>
        public async Task<Sitemap> LoadSitemapAsync(string serverUrl, string sitemapName)
        {
            var url = string.Format(Api["sitemap"], serverUrl, sitemapName);
            return (await Get(url)).ToSitemap();
        }

        /// <summary>
        /// Loads the page asynchronous.
        /// </summary>
        /// <param name="serverUrl">The server URL.</param>
        /// <param name="sitemapName">Name of the sitemap.</param>
        /// <param name="pageName">Name of the page.</param>
        /// <returns></returns>
        public async Task<Page> LoadPageAsync(string serverUrl, string sitemapName, string pageName)
        {
            var url = string.Format(Api["page"], serverUrl, sitemapName, pageName);
            return (await Get(url)).ToPage();
        }

        /// <summary>
        /// Determines whether the specified server URL is openhab2.
        /// </summary>
        /// <param name="serverUrl">The server URL.</param>
        /// <returns></returns>
        public async Task<bool> IsOpenhab2(string serverUrl)
        {
            var url = string.Format(Api["events"], serverUrl);

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

        public void SetAuthentication(string username, string password)
        {
            var plain = string.Concat(username, ":", password);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(plain));
            Authentication = string.Concat("Basic ", base64);
        }
    }
}