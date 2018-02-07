
namespace BDCompany.Views
{
    using MahApps.Metro.Controls;

    /// <summary>
    /// The main view.
    /// </summary>
    public partial class MainView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The hamburger menu control_ on item invoked.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            if ((e.InvokedItem as HamburgerMenuItem)?.Tag != null)
            {
                this.HamburgerMenuControl.SetCurrentValue(ContentProperty, (object)e.InvokedItem);
            }
        }
    }
}