using Dispatch.Common.DataHolders.Storage;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.ViewModels
{
    class _911ViewModel : ViewModelBase
    {
        private EmergencyCall _emergecnyCall;
        public EmergencyCall Call
        {
            get { return _emergecnyCall; }
            set
            {
                _emergecnyCall = value;
                RaisePropertyChanged();
            }
        }

        public _911ViewModel()
        {

        }
    }
}
