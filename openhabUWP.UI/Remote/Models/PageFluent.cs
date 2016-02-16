using System.Collections.Generic;

namespace openhabUWP.Remote.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class PageFluent
    {
        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static Page SetId(this Page page, string id)
        {
            page.Id = id;
            return page;
        }

        /// <summary>
        /// Sets the icon.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public static Page SetIcon(this Page page, string icon)
        {
            page.Icon = icon;
            return page;
        }

        /// <summary>
        /// Sets the title.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public static Page SetTitle(this Page page, string title)
        {
            page.Title = title;
            return page;
        }

        /// <summary>
        /// Sets the leaf.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="leaf">if set to <c>true</c> [leaf].</param>
        /// <returns></returns>
        public static Page SetLeaf(this Page page, bool leaf)
        {
            page.Leaf = leaf;
            return page;
        }

        /// <summary>
        /// Sets the link.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="link">The link.</param>
        /// <returns></returns>
        public static Page SetLink(this Page page, string link)
        {
            page.Link = link;
            return page;
        }

        /// <summary>
        /// Sets the widgets.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="widgets">The widgets.</param>
        /// <returns></returns>
        public static Page SetWidgets(this Page page, params Widget[] widgets)
        {
            page.Widgets = new List<Widget>(widgets);
            return page;
        }

        /// <summary>
        /// Sets the parent.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        public static Page SetParent(this Page page, Page parent)
        {
            page.Parent = parent;
            return page;
        }
    }
}