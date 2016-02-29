using System.Net;
using System.Net.Http;

namespace openhabUWP.Remote.Services
{
    public interface IHttpService
    {
        HttpClient GetClient();
        HttpClientHandler GetClientHandler();
    }

    public class HttpService : IHttpService
    {
        private HttpClient _client;
        private HttpClientHandler _clientHandler;

        public HttpClient GetClient()
        {
            if (_client != null) return _client;
            _client = new HttpClient(GetClientHandler());
            return _client;
        }

        public HttpClientHandler GetClientHandler()
        {
            if (_clientHandler != null) return _clientHandler;
            _clientHandler = new HttpClientHandler();
            if (_clientHandler.SupportsAutomaticDecompression)
            {
                _clientHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            }
            return _clientHandler;
        }
    }
}
