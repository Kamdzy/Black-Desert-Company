// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BeerCalculatorViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The beer calculator view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
                this.grainAmount = !value.Equals(0) ? value : 0;
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
                this.CalculateRequirements();
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
        [SuppressMessage(
            "StyleCop.CSharp.MaintainabilityRules",
            "SA1407:ArithmeticExpressionsMustDeclarePrecedence",
            Justification = "Reviewed. Suppression is OK here.")]
        public void CalculateRequirements()
        {
            int sugar;
            int fermenter;
            int water;
            const int Utensil = 100;

            switch (this.TierTypeName)
            {
                case "Blue":
                    sugar = 5;
                    fermenter = 10;
                    water = 30;

                    this.BeerAmount = this.GrainAmount / 5;
                    this.SugarAmount = sugar * this.BeerAmount;
                    this.FermenterAmount = fermenter * this.BeerAmount;
                    this.WaterAmount = water * this.BeerAmount;
                    break;
                case "Green":
                    sugar = 1;
                    fermenter = 2;
                    water = 6;

                    this.BeerAmount = this.GrainAmount / 5;
                    this.SugarAmount = sugar * this.BeerAmount;
                    this.FermenterAmount = fermenter * this.BeerAmount;
                    this.WaterAmount = water * this.BeerAmount;
                    break;
                case "Normal":
                    sugar = 1;
                    fermenter = 2;
                    water = 6;

                    this.BeerAmount = this.GrainAmount / 5;
                    this.SugarAmount = sugar * this.BeerAmount;
                    this.FermenterAmount = fermenter * this.BeerAmount;
                    this.WaterAmount = water * this.BeerAmount;
                    break;
            }

            if (this.BeerAmount % 100 > 0)
            {
                this.UtensilAmount = this.BeerAmount / Utensil + 1;
            }
            else
            {
                // ReSharper disable once PossibleLossOfFraction
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