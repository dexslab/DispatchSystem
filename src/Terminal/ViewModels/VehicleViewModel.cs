using Dispatch.Common.DataHolders.Storage;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.ViewModels
{
    class VehicleViewModel : ViewModelBase
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private CivilianVeh _vehicle;
        public CivilianVeh Vehicle
        {
            get { return _vehicle; }
            set
            {
                _vehicle = value;
                Name = $"{Vehicle.Owner.First} {Vehicle.Owner.Last}";
                RaisePropertyChanged();
            }
        }

        public VehicleViewModel()
        {
            Vehicle = new CivilianVeh(String.Empty);
        }
    }
}
