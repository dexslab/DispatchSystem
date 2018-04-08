using Dispatch.Common.DataHolders.Storage;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.ViewModels
{
    class OfficerViewModel : ViewModelBase
    {
        private Officer _officer;
        public Officer Ofc
        {
            get { return _officer;  }
            set
            {
                _officer = value;
                RaisePropertyChanged();
            }
        }
    }
}
