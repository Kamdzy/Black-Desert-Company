using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace BDCompany
{
    public class CustomHamburgerMenuIconItem : HamburgerMenuIconItem
    {
        public new static readonly DependencyProperty ToolTipProperty
            = DependencyProperty.Register("ToolTip",
                typeof(object),
                typeof(CustomHamburgerMenuIconItem),
                new PropertyMetadata(null));

        public new object ToolTip
        {
            get { return (object)GetValue(ToolTipProperty); }
            set { SetValue(ToolTipProperty, value); }
        }
    }
}