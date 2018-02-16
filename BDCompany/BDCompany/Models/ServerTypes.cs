
namespace BDCompany.Models
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using BDCompany.ViewModels;

    /// <summary>
    ///     The server types.
    /// </summary>
    public class ServerTypes
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BDCompany.Models.ServerTypes" /> class.
        /// </summary>
        public ServerTypes()
        {
            this.Servers = new ObservableCollection<ServerTypesItemViewModel>();

            var server1 = new ServerTypesItemViewModel { Name = "EU" };
            var server2 = new ServerTypesItemViewModel { Name = "NA" };

            this.Servers.Add(server1);
            this.Servers.Add(server2);

            this.Selected = server1;
        }

        /// <summary>
        ///     Gets or sets the selected.
        /// </summary>
        public ServerTypesItemViewModel Selected { get; set; }

        /// <summary>
        ///     Gets or sets the servers.
        /// </summary>
        public ObservableCollection<ServerTypesItemViewModel> Servers { get; set; }

        /// <summary>
        /// The select by name.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        internal void SelectByName(string p)
        {
            this.Selected = this.Servers.FirstOrDefault(s => s.Name.Equals(p));
        }
    }
}