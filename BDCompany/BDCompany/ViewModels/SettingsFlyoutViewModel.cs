
namespace BDCompany.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    using BDCompany.Models;
    using BDCompany.Properties;

    using MahApps.Metro;

    using TinyLittleMvvm;

    /// <inheritdoc />
    /// <summary>
    ///     The settings flyout view model.
    /// </summary>
    public class SettingsFlyoutViewModel : DialogViewModel
    {
        /// <summary>
        ///     The _current accent color.
        /// </summary>
        private AccentColorMenuData currentAccentColor;

        /// <summary>
        /// The server type name.
        /// </summary>
        private string serverTypeName;

        /// <summary>
        /// The node server.
        /// </summary>
        private ServerTypesItemViewModel nodeServer;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:BDCompany.ViewModels.SettingsFlyoutViewModel" /> class.
        /// </summary>
        public SettingsFlyoutViewModel()
        {
            this.List = new ServerTypes();
            this.NodeServer = this.List.Selected;

            this.AccentColors = ThemeManager.Accents.Select(
                    a => new AccentColorMenuData
 {
                                                     Name = a.Name,
                                                     ColorBrush = a.Resources["AccentColorBrush"] as Brush
                                                 })
                .ToList();
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            this.currentAccentColor = this.AccentColors.Single(accent => accent.Name == theme.Item2.Name);

            this.OkCommand = new RelayCommand(this.OnOk, () => !this.HasErrors);
            this.CancelCommand = new RelayCommand(this.Close);
            this.DarkModeCommand = new RelayCommand(this.OnDarkMode);
            this.LightModeCommand = new RelayCommand(this.OnLightMode);
        }

        /// <summary>
        ///     Gets the accent colors.
        /// </summary>
        public List<AccentColorMenuData> AccentColors { get; }

        /// <summary>
        ///     Gets the cancel command.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        ///     Gets or sets the current accent color.
        /// </summary>
        public AccentColorMenuData CurrentAccentColor
        {
            get => this.currentAccentColor;
            set
            {
                if (this.currentAccentColor != value)
                {
                    this.currentAccentColor = value;
                    var theme = ThemeManager.DetectAppStyle(Application.Current);
                    var accent = ThemeManager.GetAccent(this.currentAccentColor.Name);
                    ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the list.
        /// </summary>
        public ServerTypes List { get; set; }

        /// <summary>
        ///     Gets or sets the node server.
        /// </summary>
        public ServerTypesItemViewModel NodeServer
        {
            get => this.nodeServer;
            set
            {
                this.nodeServer = value;
                this.ServerTypeName = value.Name;
                this.ChangeServer();
                this.NotifyOfPropertyChange(() => this.NodeServer);
            }
        }

        /// <summary>
        ///     Gets or sets the tier type name.
        /// </summary>
        public string ServerTypeName
        {
            get => this.serverTypeName;
            set
            {
                this.serverTypeName = value;
                this.NotifyOfPropertyChange(() => this.ServerTypeName);
            }
        }

        /// <summary>
        ///     Gets the dark mode command.
        /// </summary>
        public ICommand DarkModeCommand { get; }

        /// <summary>
        ///     Gets the light mode command.
        /// </summary>
        public ICommand LightModeCommand { get; }

        /// <summary>
        ///     Gets the ok command.
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        ///     The on dark mode.
        /// </summary>
        private void OnDarkMode()
        {
            if (Settings.Default.DarkLightSwitch)
            {
                var theme = ThemeManager.DetectAppStyle(Application.Current);
                ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, ThemeManager.GetAppTheme("BaseDark"));
            }

            Settings.Default.Save();
        }

        /// <summary>
        ///     The on light mode.
        /// </summary>
        private void OnLightMode()
        {
            if (Settings.Default.DarkLightSwitch == false)
            {
                var theme = ThemeManager.DetectAppStyle(Application.Current);
                ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, ThemeManager.GetAppTheme("BaseLight"));
            }

            Settings.Default.Save();
        }

        /// <summary>
        /// The change server.
        /// </summary>
        private void ChangeServer()
        {
            switch (this.ServerTypeName)
            {
                case "EU":
                    Settings.Default.ServerType = true;
                    Settings.Default.Save();
                    break;
                case "NA":
                    Settings.Default.ServerType = false;
                    Settings.Default.Save();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///     The on ok.
        /// </summary>
        private void OnOk()
        {
            this.Close();
            Settings.Default.Save();
        }

        /// <summary>
        /// The select server.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        internal void SelectServer(string p)
        {
            this.List.SelectByName(p);
            this.NodeServer = this.List.Selected;
        }
    }
}