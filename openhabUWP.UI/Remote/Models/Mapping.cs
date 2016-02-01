namespace openhabUWP.Remote.Models
{
    public interface IMapping
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        string Command { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        string Label { get; set; }
    }

    public class Mapping : IMapping
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mapping"/> class.
        /// </summary>
        public Mapping() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mapping" /> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="label">The label.</param>
        public Mapping(string command, string label) : this()
        {
            this.Command = command;
            this.Label = label;
        }
    }
}
