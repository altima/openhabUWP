using System.Threading.Tasks;
using openhabUWP.Models;

namespace openhabUWP.Interfaces.Services
{
    public interface IRestService
    {
        Task<Server[]> FindLocalServersAsync();

        Task<Openhab> LoadOpenhabLinksAsync(Server server);

        Task<Sitemaps> LoadSitemapsAsync(Openhab openhab);

        Task<Models.Items> LoadItemsAsync(Openhab openhab);

        Task<Sitemap> LoadSitemapDetailsAsync(Sitemap sitemap);
    }
}