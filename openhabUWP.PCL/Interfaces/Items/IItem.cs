using System;

namespace openhabUWP.Interfaces.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        string Link { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        string Label { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IItem<T> : IItem
    {
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        T State { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Items.IItem{System.DateTime}" />
    public interface IDateTimeItem : IItem<DateTime> { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Items.IItem{System.String}" />
    public interface IGroupItem : IItem<string> { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Items.IItem{System.Decimal}" />
    public interface INumberItem : IItem<decimal> { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Items.IItem{System.Boolean}" />
    public interface ISwitchItem : IItem<bool> { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Items.IItem{System.String}" />
    public interface ILocationItem : IItem<string> { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Items.IItem{System.String}" />
    public interface IDimmerItem : IItem<string> { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Items.IItem{System.String}" />
    public interface IColorItem : IItem<string> { }
}