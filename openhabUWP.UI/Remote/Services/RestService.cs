using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;
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
        /// <param name="url">The server URL.</param>
        /// <returns></returns>
        Task<bool> Ping(string url);

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
        Task<string> PostCommand(string url, string command);

        void SetAuthentication(string username, string password);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Services.IRestService20" />
    public class RestService : IRestService
    {
        private IHttpService _httpService;

        private Dictionary<string, string> Api = new Dictionary<string, string>()
        {
            {"rest", "{0}/rest"},
            {"sitemaps", "{0}/rest/sitemaps"},
            {"sitemap", "{0}/rest/sitemaps/{1}"},
            {"page", "{0}/rest/sitemaps/{1}/{2}"},

            {"items", "{0}/rest/items"},

            {"events", "{0}/rest/events"},
        };

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

        public RestService(IHttpService httpService)
        {
            _httpService = httpService;
            SetAuthentication("", "");
        }


        /// <summary>
        /// Gets the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private async Task<string> Get(string url, string accept = "application/json")
        {
            var client = _httpService.GetClient();

            var request = Create(HttpMethod.Get, url, accept);

            var response = await client.SendAsync(request);

            //var response = await client.GetAsync(new Uri(url));

            CheckResponseHeaders(response.Headers);
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }

        private HttpRequestMessage Create(HttpMethod method, string url, string accept = "application/json")
        {
            var request = new HttpRequestMessage(method, new Uri(url));
            //set default headers
            request.Headers.Add("UserAgent", UserAgent);
            request.Headers.Add("Authentication", Authentication);
            request.Headers.Add("Accept", accept);
            request.Headers.Add("Cache-Control", new[] { "no-cache" });
            if (!X_Atmosphere_tracking_id.IsNullOrEmpty())
            {
                if (request.Headers.Contains("X-Atmosphere-tracking-id"))
                    request.Headers.Remove("X-Atmosphere-tracking-id");
                request.Headers.Add("X-Atmosphere-tracking-id", X_Atmosphere_tracking_id);
            }

            return request;
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

        private async Task<string> Post(string url, string body)
        {
            var client = _httpService.GetClient();
            var request = Create(HttpMethod.Post, url, "*/*");
            request.Content = new StringContent(body, Encoding.UTF8);

            var response = await client.SendAsync(request);

            CheckResponseHeaders(response.Headers);
            try
            {
                if (response.Headers.Contains("Location")) // do redirect hidden
                {
                    var redirectUrl = response.Headers.GetValues("Location").FirstOrDefault();
                    if (!redirectUrl.IsNullOrEmpty())
                    {
                        await Task.Delay(100); // we have to wait for the change in the openhab system
                        var newState = await Get(string.Concat(redirectUrl, "?_noCache=", Guid.NewGuid().ToString("N")), "text/plain");
                        return newState;
                    }
                }
            }
            catch { }
            return string.Empty;
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
        /// <param name="url">The server URL.</param>
        /// <returns></returns>
        public async Task<bool> Ping(string url)
        {
            try
            {
                var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(500));
                var client = _httpService.GetClient();
                var reqeust = Create(HttpMethod.Head, url, "*/*");
                var result = await client.SendAsync(reqeust, cts.Token);
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
        public Task<string> PostCommand(string url, string command)
        {
            //if (!url.EndsWith("/state")) url = string.Concat(url, "/state");
            return Post(url, command);
        }

        public void SetAuthentication(string username, string password)
        {
            var plain = string.Concat(username, ":", password);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(plain));
            Authentication = string.Concat("Basic ", base64);
        }
    }
}