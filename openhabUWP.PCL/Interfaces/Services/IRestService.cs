using System;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Items;
using openhabUWP.Models;
using openhabUWP.Widgets;

namespace openhabUWP.Interfaces.Services
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
        Task PostCommand(IItem item, string command);

    }
}