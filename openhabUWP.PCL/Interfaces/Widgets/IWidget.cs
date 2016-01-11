using Windows.Devices.Bluetooth.Advertisement;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Items;
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
        /// Gets or sets the mappings.
        /// </summary>
        /// <value>
        /// The mappings.
        /// </value>
        Mapping[] Mappings { get; set; }

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

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.IWidget" />
    public interface ISelectionWidget : IWidget { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.IWidget" />
    public interface IMapViewWidget : IWidget
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        IItem Item { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        double Height { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.IWidget" />
    public interface ISetPointWidget : IWidget
    {

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        decimal Min { get; set; }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        decimal Max { get; set; }

        /// <summary>
        /// Gets or sets the step.
        /// </summary>
        /// <value>
        /// The step.
        /// </value>
        decimal Step { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.IWidget" />
    public interface IChartWidget : IWidget
    {

        /// <summary>
        /// Gets or sets the refresh.
        /// </summary>
        /// <value>
        /// The refresh.
        /// </value>
        int Refresh { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        string Period { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.IWidget" />
    public interface IUrlWidget : IWidget
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        string Url { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.IUrlWidget" />
    public interface IImageWidget : IUrlWidget { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.IUrlWidget" />
    public interface IVideoWidget : IUrlWidget { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.IUrlWidget" />
    public interface IWebViewWidget : IUrlWidget { }

}