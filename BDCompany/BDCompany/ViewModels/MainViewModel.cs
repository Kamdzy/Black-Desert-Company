using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using TinyLittleMvvm;

namespace BDCompany.ViewModels
{
    public class MainViewModel : PropertyChangedBase, IShell, IOnLoadedHandler, ICancelableOnClosingHandler
    {
        private readonly IDialogManager _dialogManager;
        private string _title;
        private static PerformanceCounter _cpuCounter;
        private bool _cpuThreadActive;

        public MainViewModel(IDialogManager dialogManager, IFlyoutManager flyoutManager)
        {
            _dialogManager = dialogManager;
            Flyouts = flyoutManager;
            ShowSampleDialogCommand = new AsyncRelayCommand(OnShowSampleDialogAsync);
            ShowSampleFlyoutCommand = new AsyncRelayCommand(OnShowSampleFlyoutAsync);

            _cpuCounter = new PerformanceCounter
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            };

            _cpuThreadActive = true;
            Thread th = new Thread(() =>
            {
                while (_cpuThreadActive)
                {
                    var cpuUsageInt = Convert.ToInt32(CurrentCpUusage);
                    Title = "Black Desert Company " + "- Total CPU Usage: " + cpuUsageInt + "%";
                    Thread.Sleep(500);
                }
            });

            th.Start();
        }

        public Task OnLoadedAsync()
        {
            Title = "Black Desert Company - Total CPU Usage: ??%";
 
            return Task.FromResult(0);
        }

        public bool OnClosing()
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Quit",
                NegativeButtonText = "Cancel",
                AnimateShow = true,
                AnimateHide = false
            };

            _dialogManager.ShowMessageBox("Quit application?",
                    "Sure you want to quit application?",
                    MessageDialogStyle.AffirmativeAndNegative, mySettings)
                .ContinueWith(t =>
                {
                    if (t.Result == MessageDialogResult.Affirmative)
                    {
                        _cpuThreadActive = false;
                        Application.Current.Shutdown();
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            return true;
        }

        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public static float CurrentCpUusage => _cpuCounter.NextValue();

        public ICommand ShowSampleDialogCommand { get; }

        public ICommand ShowSampleFlyoutCommand { get; }

        public IFlyoutManager Flyouts { get; }

        private async Task OnShowSampleDialogAsync()
        {
            var text = await _dialogManager.ShowDialogAsync<SampleDialogViewModel, string>();
            if (text != null)
            {
                await _dialogManager.ShowMessageBox(Title, "You entered: " + text);
            }
        }

        private Task OnShowSampleFlyoutAsync()
        {
            return Flyouts.ShowFlyout<SettingsFlyoutViewModel>();
        }

    }
}