
namespace BDCompany.ViewModels
{
    using System.Windows.Input;

    using TinyLittleMvvm;

    /// <summary>
    ///     The sample dialog view model.
    /// </summary>
    public class SampleDialogViewModel : DialogViewModel<string>
    {
        /// <summary>
        ///     The _text.
        /// </summary>
        private string text;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SampleDialogViewModel" /> class.
        /// </summary>
        public SampleDialogViewModel()
        {
            this.OkCommand = new RelayCommand(this.OnOk, this.CanOk);
            this.CancelCommand = new RelayCommand(this.OnCancel);
            this.text = string.Empty;

            this.AddValidationRule(() => this.Text, text => !string.IsNullOrEmpty(text), "Text must not be empty");
        }

        /// <summary>
        ///     Gets the cancel command.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        ///     Gets the ok command.
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        public string Text
        {
            get => this.text;
            set
            {
                if (this.text == value)
                {
                    return;
                }

                this.text = value;
                this.ValidateAllRules();
                this.NotifyOfPropertyChange(() => this.Text);
            }
        }

        /// <summary>
        ///     The can ok.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        private bool CanOk()
        {
            return !this.HasErrors;
        }

        /// <summary>
        ///     The on cancel.
        /// </summary>
        private void OnCancel()
        {
            this.Close(null);
        }

        /// <summary>
        ///     The on ok.
        /// </summary>
        private void OnOk()
        {
            this.Close(this.Text);
        }
    }
}