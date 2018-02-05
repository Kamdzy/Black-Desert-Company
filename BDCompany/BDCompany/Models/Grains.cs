using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDCompany.ViewModels;

namespace BDCompany.Models
{
    public class GrainTypes
    {
        public ObservableCollection<GrainTypesItemViewModel> Grains { get; set; }
        public GrainTypesItemViewModel Selected { get; set; }
        public GrainTypes()
        {
            Grains = new ObservableCollection<GrainTypesItemViewModel>();

            var grain1 = new GrainTypesItemViewModel() { Name = "Grain 1" };
            var grain2 = new GrainTypesItemViewModel() { Name = "Grain 2" };
            var grain3 = new GrainTypesItemViewModel() { Name = "Grain 3" };
            var grain4 = new GrainTypesItemViewModel() { Name = "Grain 4" };

            Grains.Add(grain1);
            Grains.Add(grain2);
            Grains.Add(grain3);
            Grains.Add(grain4);

            this.Selected = grain3;
        }

        internal void SelectByName(string p)
        {
            this.Selected = this.Grains.FirstOrDefault(s => s.Name.Equals(p));
        }
    }
}
