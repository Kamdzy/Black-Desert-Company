using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyLittleMvvm;

namespace BDCompany.ViewModels
{
    public class GrainTypesItemViewModel : PropertyChangedBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
    }
}
