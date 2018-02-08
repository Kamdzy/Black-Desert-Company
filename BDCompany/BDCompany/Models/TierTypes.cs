
namespace BDCompany.Models
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using BDCompany.ViewModels;

    /// <summary>
    ///     The tier types.
    /// </summary>
    public class TierTypes
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TierTypes" /> class.
        /// </summary>
        public TierTypes()
        {
            this.Tiers = new ObservableCollection<TierTypesItemViewModel>();

            var tier1 = new TierTypesItemViewModel { Name = "Normal/Green" };
            var tier2 = new TierTypesItemViewModel { Name = "Blue" };

            this.Tiers.Add(tier1);
            this.Tiers.Add(tier2);

            this.Selected = tier1;
        }

        /// <summary>
        ///     Gets or sets the selected.
        /// </summary>
        public TierTypesItemViewModel Selected { get; set; }

        /// <summary>
        ///     Gets or sets the tiers.
        /// </summary>
        public ObservableCollection<TierTypesItemViewModel> Tiers { get; set; }

        /// <summary>
        /// The select by name.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        internal void SelectByName(string p)
        {
            this.Selected = this.Tiers.FirstOrDefault(s => s.Name.Equals(p));
        }
    }
}