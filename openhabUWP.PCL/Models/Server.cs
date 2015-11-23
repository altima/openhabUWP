using openhabUWP.Enums;
using openhabUWP.Interfaces.Common;

namespace openhabUWP.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Server : ILinkItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        public Server()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public Server(string url) : this()
        {
            if (!url.EndsWith("/rest")) url = string.Concat(url, "/rest");
            if (!url.StartsWith("http")) url = string.Concat("http://", url);
            this.Link = url;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        public Server(ProtocolType protocol, string host, int port = 8080, string path = "/rest") : this()
        {
            if (host.Contains("://")) host = host.Substring(host.IndexOf("://") + 2);
            this.Link = string.Concat(protocol, "://", host, ":", port, path);
        }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        public string Link { get; set; }
    }
}
