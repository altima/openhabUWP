namespace openhabUWP.Remote.Models
{
    public static class MappingFluent
    {
        /// <summary>
        /// Sets the command.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public static Mapping SetCommand(this Mapping mapping, string command)
        {
            mapping.Command = command;
            return mapping;
        }

        /// <summary>
        /// Sets the label.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public static Mapping SetLabel(this Mapping mapping, string label)
        {
            mapping.Label = label;
            return mapping;
        }
    }
}