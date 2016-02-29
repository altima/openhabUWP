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

        /// <summary>
        /// Sets the tags.
        /// </summary>
        /// <param name="item">The widget.</param>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        public static Item SetTags(this Item item, string[] tags)
        {
            item.Tags = tags;
            return item;
        }

        /// <summary>
        /// Sets the group names.
        /// </summary>
        /// <param name="item">The widget.</param>
        /// <param name="groupNames">The group names.</param>
        /// <returns></returns>
        public static Item SetGroupNames(this Item item, string[] groupNames)
        {
            item.GroupNames = groupNames;
            return item;
        }


        /// <summary>
        /// Determines whether the specified type is item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static bool IsItem(this Item item, string type)
        {
            return Equals(item.Type, type);
        }

        /// <summary>
        /// Determines whether [is switch item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static bool IsSwitchItem(this Item item)
        {
            return item.IsItem("SwitchItem");
        }

        /// <summary>
        /// Determines whether [is rollershutter item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static bool IsRollershutterItem(this Item item)
        {
            return item.IsItem("RollershutterItem");
        }

        /// <summary>
        /// Determines whether [is number item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static bool IsNumberItem(this Item item)
        {
            return item.IsItem("NumberItem");
        }

        /// <summary>
        /// Determines whether [is contact item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static bool IsContactItem(this Item item)
        {
            return item.IsItem("ContactItem");
        }

        /// <summary>
        /// Determines whether [is group item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static bool IsGroupItem(this Item item)
        {
            return item.IsItem("GroupItem");
        }

        /// <summary>
        /// Determines whether [is date time item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static bool IsDateTimeItem(this Item item)
        {
            return item.IsItem("DateTimeItem");
        }

        /// <summary>
        /// Determines whether [is dimmer item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static bool IsDimmerItem(this Item item)
        {
            return item.IsItem("DimmerItem");
        }

        /// <summary>
        /// Determines whether [is color item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static bool IsColorItem(this Item item)
        {
            return item.IsItem("ColorItem");
        }

        /// <summary>
        /// Determines whether [is location item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static bool IsLocationItem(this Item item)
        {
            return item.IsItem("LocationItem");
        }
    }
}