namespace openhabUWP.Remote.Models
{
    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// Sets the state description.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="stateDescription">The state description.</param>
        /// <returns></returns>
        public static Item SetStateDescription(this Item item, StateDescription stateDescription)
        {
            item.StateDescription = stateDescription;
            return item;
        }

        /// <summary>
        /// Sets the icon.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public static Item SetIcon(this Item item, string icon)
        {
            item.Icon = icon;
            return item;
        }

        /// <summary>
        /// Sets the label.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public static Item SetLabel(this Item item, string label)
        {
            item.Label = label;
            return item;
        }

        /// <summary>
        /// Sets the category.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public static Item SetCategory(this Item item, string category)
        {
            item.Category = category;
            return item;
        }
    }
}