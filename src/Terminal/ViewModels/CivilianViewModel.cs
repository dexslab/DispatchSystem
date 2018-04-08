using Dispatch.Common.DataHolders.Storage;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.ViewModels 
{
    class CivilianViewModel : ViewModelBase
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

        private Civilian _civilian;
        public Civilian Civ
        {
            get { return _civilian; }
            set
            {
                _civilian = value;
                Name = $"{Civ.First} {Civ.Last}";
                RaisePropertyChanged();
            }
        }

        public CivilianViewModel(Civilian civ)
        {
            Civ = civ;
        }

        public CivilianViewModel()
        {
            Civ = new Civilian("none");
        }
    }
}
