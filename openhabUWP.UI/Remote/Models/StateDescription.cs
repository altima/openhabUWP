namespace openhabUWP.Remote.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class StateDescription
    {
        /// <summary>
        /// Gets or sets the pattern.
        /// </summary>
        /// <value>
        /// The pattern.
        /// </value>
        public string Pattern { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [read only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [read only]; otherwise, <c>false</c>.
        /// </value>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public string[] Options { get; set; } //????

        /// <summary>
        /// Initializes a new instance of the <see cref="StateDescription"/> class.
        /// </summary>
        public StateDescription() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateDescription"/> class.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        public StateDescription(string pattern, bool readOnly)
            : this()
        {
            this.Pattern = pattern;
            this.ReadOnly = readOnly;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateDescription"/> class.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        /// <param name="options">The options.</param>
        public StateDescription(string pattern, bool readOnly, string[] options)
            : this(pattern, readOnly)
        {
            this.Options = options;
        }
    }
}
