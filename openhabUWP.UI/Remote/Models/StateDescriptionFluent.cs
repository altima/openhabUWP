namespace openhabUWP.Remote.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class StateDescriptionFluent
    {
        /// <summary>
        /// Sets the pattern.
        /// </summary>
        /// <param name="stateDescription">The state description.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        public static StateDescription SetPattern(this StateDescription stateDescription, string pattern)
        {
            stateDescription.Pattern = pattern;
            return stateDescription;
        }

        /// <summary>
        /// Sets the read only.
        /// </summary>
        /// <param name="stateDescription">The state description.</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        /// <returns></returns>
        public static StateDescription SetReadOnly(this StateDescription stateDescription, bool readOnly)
        {
            stateDescription.ReadOnly = readOnly;
            return stateDescription;
        }
    }
}