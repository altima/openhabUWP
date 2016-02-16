namespace openhabUWP.Remote.Models
{
    public static class ItemFluent
    {
        /// <summary>
        /// Sets the link.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="link">The link.</param>
        /// <returns></returns>
        public static Item SetLink(this Item item, string link)
        {
            item.Link = link;
            return item;
        }

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static Item SetName(this Item item, string name)
        {
            item.Name = name;
            return item;
        }

        /// <summary>
        /// Sets the state.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public static Item SetState(this Item item, string state)
        {
            item.State = state;
            return item;
        }

        /// <summary>
        /// Sets the type.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static Item SetType(this Item item, string type)
        {
            item.Type = type;
            return item;
        }
    }
}