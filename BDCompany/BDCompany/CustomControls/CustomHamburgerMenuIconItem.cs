
namespace BDCompany.CustomControls
{
    using System.Windows;

    using MahApps.Metro.Controls;

    /// <summary>
    /// The custom hamburger menu icon item.
    /// </summary>
    public class CustomHamburgerMenuIconItem : HamburgerMenuIconItem
    {
        /// <summary>
        /// The tool tip property.
        /// </summary>
        public static new readonly DependencyProperty ToolTipProperty = DependencyProperty.Register(
            "ToolTip",
            typeof(object),
            typeof(CustomHamburgerMenuIconItem),
            new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the tool tip.
        /// </summary>
        public new object ToolTip
        {
            get => this.GetValue(ToolTipProperty);
            set => this.SetValue(ToolTipProperty, value);
        }
    }
}