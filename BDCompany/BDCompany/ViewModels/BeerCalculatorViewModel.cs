using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDCompany.Models;
using TinyLittleMvvm;

namespace BDCompany.ViewModels
{
    public class BeerCalculatorViewModel : PropertyChangedBase, IShell
    {
        public GrainTypes List { get; set; }

        public BeerCalculatorViewModel()
        {
            this.List = new GrainTypes();
            NodeGrain = List.Selected;
        }

        private GrainTypesItemViewModel _nodeGrain;
        public GrainTypesItemViewModel NodeGrain
        {
            get
            {
                return _nodeGrain;
            }
            set
            {
                _nodeGrain = value;
                NotifyOfPropertyChange(() => NodeGrain);
            }
        }

        internal void SelectGrain(string p)
        {
            this.List.SelectByName(p);
            this.NodeGrain = this.List.Selected;
        }
    }
}
