using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro;
using MahApps.Metro.Controls;
using TinyLittleMvvm;

namespace BDCompany.ViewModels
{
    public class SettingsFlyoutViewModel : DialogViewModel
    {

        private AccentColorMenuData _currentAccentColor;

        public SettingsFlyoutViewModel()
        {
            AccentColors = ThemeManager.Accents
                .Select(a => new AccentColorMenuData
                {
                    Name = a.Name,
                    ColorBrush = a.Resources["AccentColorBrush"] as Brush
                })
                .ToList();
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            _currentAccentColor = AccentColors.Single(accent => accent.Name == theme.Item2.Name);

            OkCommand = new RelayCommand(OnOk, () => !HasErrors);
            CancelCommand = new RelayCommand(Close);
            DarkModeCommand = new RelayCommand(OnDarkMode);
            LightModeCommand = new RelayCommand(OnLightMode);
        }

        public List<AccentColorMenuData> AccentColors { get; }

        public AccentColorMenuData CurrentAccentColor
        {
            get { return _currentAccentColor; }
            set
            {
                if (_currentAccentColor != value)
                {
                    _currentAccentColor = value;
                    var theme = ThemeManager.DetectAppStyle(Application.Current);
                    var accent = ThemeManager.GetAccent(_currentAccentColor.Name);
                    ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
                }
            }
        }

        public ICommand LightModeCommand { get; }

        public ICommand DarkModeCommand { get; }

        public ICommand OkCommand { get; }

        public ICommand CancelCommand { get; }

        private void OnOk()
        {
            Close();
            Properties.Settings.Default.Save();
        }

        private void OnDarkMode()
        {
            if (Properties.Settings.Default.DarkLightSwitch == true)
            {
                var theme = ThemeManager.DetectAppStyle(Application.Current);
                ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, ThemeManager.GetAppTheme("BaseDark"));
            }

            Properties.Settings.Default.Save();
        }

        private void OnLightMode()
        {
            if (Properties.Settings.Default.DarkLightSwitch == false)
            {
                var theme = ThemeManager.DetectAppStyle(Application.Current);
                ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, ThemeManager.GetAppTheme("BaseLight"));
            }


            Properties.Settings.Default.Save();
        }
    }

    public class AccentColorMenuData
    {
        public string Name { get; set; }
        public Brush ColorBrush { get; set; }
    }
}