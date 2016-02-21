namespace openhabUWP.Database
{
    public interface ITable
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        int Id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Database.ITable" />
    public class Setup : ITable
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the remote URL.
        /// </summary>
        /// <value>
        /// The remote URL.
        /// </value>
        public string RemoteUrl { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [ignore SSL certificate].
        /// </summary>
        /// <value>
        /// <c>true</c> if [ignore SSL certificate]; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreSslCertificate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [ignore SSL hostnames].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ignore SSL hostnames]; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreSslHostnames { get; set; }

        /// <summary>
        /// Gets or sets the sitemap.
        /// </summary>
        /// <value>
        /// The sitemap.
        /// </value>
        public string Sitemap { get; set; }
    }
}
