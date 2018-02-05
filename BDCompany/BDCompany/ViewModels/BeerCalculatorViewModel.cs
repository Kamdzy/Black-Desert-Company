
namespace BDCompany.ViewModels
{
    using System.Diagnostics.CodeAnalysis;

    using BDCompany.Models;

    using TinyLittleMvvm;

    /// <summary>
    ///     The beer calculator view model.
    /// </summary>
    public class BeerCalculatorViewModel : PropertyChangedBase, IShell
    {
        /// <summary>
        ///     The beer amount.
        /// </summary>
        private int beerAmount;

        /// <summary>
        ///     The fermenter amount.
        /// </summary>
        private int fermenterAmount;

        /// <summary>
        ///     The grain amount.
        /// </summary>
        private int grainAmount;

        /// <summary>
        ///     The _node tier.
        /// </summary>
        private TierTypesItemViewModel nodeTier;

        /// <summary>
        ///     The _sugar amount.
        /// </summary>
        private int sugarAmount;

        /// <summary>
        ///     The tier type name.
        /// </summary>
        private string tierTypeName;

        /// <summary>
        ///     The utensil amount.
        /// </summary>
        private float utensilAmount;

        /// <summary>
        ///     The water amount.
        /// </summary>
        private int waterAmount;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BeerCalculatorViewModel" /> class.
        /// </summary>
        public BeerCalculatorViewModel()
        {
            this.List = new TierTypes();
            this.NodeTier = this.List.Selected;
        }

        /// <summary>
        ///     Gets or sets the beer amount.
        /// </summary>
        public int BeerAmount
        {
            get => this.beerAmount;
            set
            {
                this.beerAmount = value;
                this.NotifyOfPropertyChange(() => this.BeerAmount);
            }
        }

        /// <summary>
        ///     Gets or sets the fermenter amount.
        /// </summary>
        public int FermenterAmount
        {
            get => this.fermenterAmount;
            set
            {
                this.fermenterAmount = value;
                this.NotifyOfPropertyChange(() => this.FermenterAmount);
            }
        }

        /// <summary>
        ///     Gets or sets the grain amount.
        /// </summary>
        public int GrainAmount
        {
            get => this.grainAmount;
            set
            {
                this.grainAmount = value;
                this.CalculateRequirements();
                this.NotifyOfPropertyChange(() => this.GrainAmount);
            }
        }

        /// <summary>
        ///     Gets or sets the list.
        /// </summary>
        public TierTypes List { get; set; }

        /// <summary>
        ///     Gets or sets the node tier.
        /// </summary>
        public TierTypesItemViewModel NodeTier
        {
            get => this.nodeTier;
            set
            {
                this.nodeTier = value;
                this.TierTypeName = value.Name;
                this.NotifyOfPropertyChange(() => this.NodeTier);
            }
        }

        /// <summary>
        ///     Gets or sets the sugar amount.
        /// </summary>
        public int SugarAmount
        {
            get => this.sugarAmount;
            set
            {
                this.sugarAmount = value;
                this.NotifyOfPropertyChange(() => this.SugarAmount);
            }
        }

        /// <summary>
        ///     Gets or sets the tier type name.
        /// </summary>
        public string TierTypeName
        {
            get => this.tierTypeName;
            set
            {
                this.tierTypeName = value;
                this.NotifyOfPropertyChange(() => this.TierTypeName);
            }
        }

        /// <summary>
        ///     Gets or sets the utensil amount.
        /// </summary>
        public float UtensilAmount
        {
            get => this.utensilAmount;
            set
            {
                this.utensilAmount = value;
                this.NotifyOfPropertyChange(() => this.UtensilAmount);
            }
        }

        /// <summary>
        ///     Gets or sets the water amount.
        /// </summary>
        public int WaterAmount
        {
            get => this.waterAmount;
            set
            {
                this.waterAmount = value;
                this.NotifyOfPropertyChange(() => this.WaterAmount);
            }
        }

        /// <summary>
        ///     The calculate requirements.
        /// </summary>
        public void CalculateRequirements()
        {
            const int Sugar = 5;
            const int Fermenter = 2;
            const int Water = 6;
            const int Utensil = 100;

            this.BeerAmount = this.GrainAmount / 5;
            this.SugarAmount = Sugar * this.BeerAmount;
            this.FermenterAmount = Fermenter * this.BeerAmount;
            this.WaterAmount = Water * this.BeerAmount;

            if (this.BeerAmount % 100 > 0)
            {
                this.UtensilAmount = this.BeerAmount / Utensil + 1;
            }
            else
            {
                this.UtensilAmount = this.BeerAmount / Utensil;
            }
        }

        /// <summary>
        /// The select grain.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        internal void SelectGrain(string p)
        {
            this.List.SelectByName(p);
            this.NodeTier = this.List.Selected;
        }
    }
}