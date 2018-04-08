using Dispatch.Common.DataHolders.Storage;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.ViewModels
{
    class BoloViewModel : ViewModelBase
    {
        private Bolo _bolo;
        public Bolo Bolo
        {
            get { return _bolo; }
            set
            {
                _bolo = value;
                RaisePropertyChanged();
            }
        }
    }
}
