using openhabUWP.Interfaces.Common;
using openhabUWP.Models;

namespace openhabUWP.Interfaces.Widgets
{
    public interface IWidget
    {
        /// <summary>
        /// Gets or sets the widget identifier.
        /// </summary>
        /// <value>
        /// The widget identifier.
        /// </value>
        string WidgetId { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value> 
        string Icon { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        string Label { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets the linked page.
        /// </summary>
        /// <value>
        /// The linked page.
        /// </value>
        Page LinkedPage { get; set; }
        
        /// <summary>
        /// Gets or sets the widgets.
        /// </summary>
        /// <value>
        /// The widgets.
        /// </value>
        IWidget[] Widgets { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IWidget" />
    public interface IItemWidget : IWidget { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IWidget" />
    public interface IFrameWidget : IWidget { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IWidget" />
    public interface ITextWidget : IWidget
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        IItem Item { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IWidget" />
    public interface ISwitchWidget : IWidget
    {

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        IItem Item { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IWidget" />
    public interface IGroupWidget : IWidget { }
}