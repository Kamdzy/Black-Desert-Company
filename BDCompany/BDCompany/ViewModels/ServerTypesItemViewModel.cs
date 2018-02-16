
namespace BDCompany.ViewModels
{
    using System;

    using TinyLittleMvvm;

    /// <summary>
    ///     The server types item view model.
    /// </summary>
    public class ServerTypesItemViewModel : PropertyChangedBase
    {
        /// <summary>
        ///     The _name.
        /// </summary>
        private string name;

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => this.name;
            set
            {
                this.name = value;
                this.NotifyOfPropertyChange(() => this.Name);
            }
        }
    }
}