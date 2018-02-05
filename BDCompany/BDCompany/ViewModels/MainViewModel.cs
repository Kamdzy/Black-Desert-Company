
namespace BDCompany.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    using BDCompany.Properties;

    using MahApps.Metro;
    using MahApps.Metro.Controls.Dialogs;

    using TinyLittleMvvm;

    /// <inheritdoc cref="ClassHandlers" />
    /// <summary>
    ///     The main view model.
    /// </summary>
    public class MainViewModel : PropertyChangedBase, IShell, IOnLoadedHandler, ICancelableOnClosingHandler
    {
        /// <summary>
        ///     The CPU counter.
        /// </summary>
        private static PerformanceCounter cpuCounter;

        /// <summary>
        ///     The _dialog manager.
        /// </summary>
        private readonly IDialogManager dialogManager;

        /// <summary>
        ///     The CPU thread active.
        /// </summary>
        private bool cpuThreadActive;

        /// <summary>
        ///     The _title.
        /// </summary>
        private string title;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="dialogManager">
        /// The dialog manager.
        /// </param>
        /// <param name="flyoutManager">
        /// The flyout manager.
        /// </param>
        public MainViewModel(IDialogManager dialogManager, IFlyoutManager flyoutManager)
        {
            if (Settings.Default.DarkLightSwitch)
            {
                var theme = ThemeManager.DetectAppStyle(Application.Current);
                ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, ThemeManager.GetAppTheme("BaseDark"));
            }

            this.dialogManager = dialogManager;
            this.Flyouts = flyoutManager;
            this.ShowSampleDialogCommand = new AsyncRelayCommand(this.OnShowSampleDialogAsync);
            this.ShowSampleFlyoutCommand = new AsyncRelayCommand(this.OnShowSampleFlyoutAsync);

            cpuCounter = new PerformanceCounter
                              {
                                  CategoryName = "Processor",
                                  CounterName = "% Processor Time",
                                  InstanceName = "_Total"
                              };

            this.cpuThreadActive = true;
            var th = new Thread(
                () =>
                    {
                        while (this.cpuThreadActive)
                        {
                            var cpuUsageInt = Convert.ToInt32(CurrentCpUusage);
                            this.Title = "Black Desert Company " + "- Total CPU Usage: " + cpuUsageInt + "%";
                            Thread.Sleep(500);
                        }
                    });

            th.Start();
        }

        /// <summary>
        ///    Gets the current CPU usage.
        /// </summary>
        public static float CurrentCpUusage => cpuCounter.NextValue();

        /// <summary>
        ///     Gets the flyouts.
        /// </summary>
        public IFlyoutManager Flyouts { get; }

        /// <summary>
        ///     Gets the show sample dialog command.
        /// </summary>
        public ICommand ShowSampleDialogCommand { get; }

        /// <summary>
        ///     Gets the show sample flyout command.
        /// </summary>
        public ICommand ShowSampleFlyoutCommand { get; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        public string Title
        {
            get => this.title;
            set
            {
                if (this.title == value)
                {
                    return;
                }

                this.title = value;
                this.NotifyOfPropertyChange(() => this.Title);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     The on closing.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Boolean" />.
        /// </returns>
        public bool OnClosing()
        {
            var mySettings = new MetroDialogSettings
                                 {
                                     AffirmativeButtonText = "Quit",
                                     NegativeButtonText = "Cancel",
                                     AnimateShow = true,
                                     AnimateHide = false
                                 };

            this.dialogManager.ShowMessageBox(
                "Quit application?",
                "Sure you want to quit application?",
                MessageDialogStyle.AffirmativeAndNegative,
                mySettings).ContinueWith(
                t =>
                    {
                        if (t.Result == MessageDialogResult.Affirmative)
                        {
                            this.cpuThreadActive = false;
                            Application.Current.Shutdown();
                        }
                    },
                TaskScheduler.FromCurrentSynchronizationContext());
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     The on loaded async.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Threading.Tasks.Task" />.
        /// </returns>
        public Task OnLoadedAsync()
        {
            this.Title = "Black Desert Company - Total CPU Usage: ??%";

            return Task.FromResult(0);
        }

        /// <summary>
        ///     The on show sample dialog async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        private async Task OnShowSampleDialogAsync()
        {
            var text = await this.dialogManager.ShowDialogAsync<SampleDialogViewModel, string>();
            if (text != null)
            {
                await this.dialogManager.ShowMessageBox(this.Title, "You entered: " + text);
            }
        }

        /// <summary>
        ///     The on show sample flyout async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        private Task OnShowSampleFlyoutAsync()
        {
            return this.Flyouts.ShowFlyout<SettingsFlyoutViewModel>();
        }
    }
}