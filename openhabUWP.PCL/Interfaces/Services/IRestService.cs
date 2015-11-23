using System;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using openhabUWP.Interfaces.Common;
using openhabUWP.Models;
using openhabUWP.Widgets;

namespace openhabUWP.Interfaces.Services
{
    public interface IRestService
    {
        /// <summary>
        /// Finds the local servers asynchronous.
        /// </summary>
        /// <returns cref="Server"></returns>
        Task<Server[]> FindLocalServersAsync();

        /// <summary>
        /// Loads the openhab links asynchronous.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns cref="Openhab"></returns>
        Task<Openhab> LoadOpenhabLinksAsync(Server server);

        /// <summary>
        /// Loads the sitemaps asynchronous.
        /// </summary>
        /// <param name="openhab">The openhab.</param>
        /// <returns cref="Sitemaps"></returns>
        Task<Sitemaps> LoadSitemapsAsync(Openhab openhab);

        /// <summary>
        /// Loads the items asynchronous.
        /// </summary>
        /// <param name="openhab">The openhab.</param>
        /// <returns cref="Models.Items"></returns>
        Task<Models.Items> LoadItemsAsync(Openhab openhab);

        /// <summary>
        /// Loads the sitemap details asynchronous.
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <returns cref="Sitemap"></returns>
        Task<Sitemap> LoadSitemapDetailsAsync(Sitemap sitemap);

        /// <summary>
        /// Registers the web socket asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="callbackAction">The callback action.</param>
        /// <returns cref="MessageWebSocket"></returns>
        Task<MessageWebSocket> RegisterWebSocketAsync(string url, Action<MessageWebSocketMessageReceivedEventArgs> callbackAction);

        /// <summary>
        /// Registers the web socket asynchronous.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="callbackAction">The callback action.</param>
        /// <returns></returns>
        Task<MessageWebSocket> RegisterWebSocketAsync(IPage page, Action<MessageWebSocketMessageReceivedEventArgs> callbackAction);

        /// <summary>
        /// Registers the web socket asynchronous.
        /// </summary>
        /// <param name="openhab">The openhab.</param>
        /// <param name="callbackAction">The callback action.</param>
        /// <returns></returns>
        Task<MessageWebSocket> RegisterWebSocketAsync(Openhab openhab, Action<MessageWebSocketMessageReceivedEventArgs> callbackAction);
    }
}