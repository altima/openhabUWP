using openhabUWP.Remote.Models;
using Prism.Events;

namespace openhabUWP.Events
{
    public class SitemapEvents
    {
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Prism.Events.PubSubEvent{openhabUWP.Remote.Models.Sitemap}" />
        public class TappedEvent : PubSubEvent<Sitemap> { }
    }
}