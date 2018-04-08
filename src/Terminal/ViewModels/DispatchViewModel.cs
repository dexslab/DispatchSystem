using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dispatch.Common.DataHolders.Storage;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Terminal.ViewModels
{
    class DispatchViewModel : ViewModelBase, IDispatch
    {
        IMainViewModel _mainViewModel;

        #region Properties / Commands
        Assignment _selectedAssignment;
        public Assignment SelectedAssignment
        {
            get { return _selectedAssignment; }
            set
            {
                _selectedAssignment = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<Assignment> _assignments;
        public ObservableCollection<Assignment> Assignments
        {
            get { return _assignments; }
            set
            {
                _assignments = value;
                RaisePropertyChanged("Assignments");
            }
        }

        Bolo _selectedBolo;
        public Bolo SelectedBolo
        {
            get { return _selectedBolo; }
            set
            {
                _selectedBolo = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<Bolo> _bolos;
        public ObservableCollection<Bolo> Bolos
        {
            get { return _bolos; }
            set
            {
                _bolos = value;
                RaisePropertyChanged("Bolos");
            }
        }

        Bolo _selectedCivilian;
        public Bolo SelectedCivilian
        {
            get { return _selectedCivilian; }
            set
            {
                _selectedCivilian = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Civilian> BackupCivs;
        public bool CivsBackedup { get; set; }
        ObservableCollection<Civilian> _civilians;
        public ObservableCollection<Civilian> Civilians
        {
            get { return _civilians; }
            set
            {
                _civilians = value;
                if (!CivsBackedup)
                {
                    BackupCivs = Civilians;
                    CivsBackedup = true;
                }
                RaisePropertyChanged("Civilians");
            }
        }

        private string _civSearch;
        public string CivSearch
        {
            get { return _civSearch; }
            set
            {
                _civSearch = value;
                if (String.IsNullOrEmpty(CivSearch))
                {
                    Civilians = BackupCivs;
                }
                ExecuteSearch();
                RaisePropertyChanged();
            }
        }

        private string _vehSearch;
        public string VehSearch
        {
            get { return _vehSearch; }
            set
            {
                _civSearch = value;
                if (String.IsNullOrEmpty(_vehSearch))
                {
                    Vehicles = BackupVehs;
                }
                ExecuteSearch();
                RaisePropertyChanged();
            }
        }



        Bolo _selectedVehicle;
        public Bolo SelectedVehicle
        {
            get { return _selectedVehicle; }
            set
            {
                _selectedVehicle = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<CivilianVeh> BackupVehs;
        public bool VehsBackedup { get; set; }
        ObservableCollection<CivilianVeh> _vehicles;
        public ObservableCollection<CivilianVeh> Vehicles
        {
            get { return _vehicles; }
            set
            {
                _vehicles = value;
                if (!VehsBackedup)
                {
                    BackupCivs = Civilians;
                    VehsBackedup = true;
                }
                RaisePropertyChanged("Vehicles");
            }
        }

        ObservableCollection<Officer> _officers;
        public ObservableCollection<Officer> Officers
        {
            get { return _officers; }
            set
            {
                _officers = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand<Civilian> _showCivilian;
        public ICommand ShowCivilian
        {
            get
            {
                if (_showCivilian == null)
                {
                    _showCivilian = new RelayCommand<Civilian>(new Action<Civilian>(DisplayCivilian));
                }

                return _showCivilian;
            }
        }

        private RelayCommand<Assignment> _showAssignment;
        public ICommand ShowAssignment
        {
            get
            {
                if (_showAssignment == null)
                {
                    _showAssignment = new RelayCommand<Assignment>(new Action<Assignment>(DisplayAssignment));
                }

                return _showAssignment;
            }
        }


        private RelayCommand<Bolo> _showBolo;
        public ICommand ShowBolo
        {
            get
            {
                if (_showBolo == null)
                {
                    _showBolo = new RelayCommand<Bolo>(new Action<Bolo>(DisplayBolo));
                }

                return _showBolo;
            }
        }


        private RelayCommand<Officer> _showOfficer;
        public ICommand ShowOfficer
        {
            get
            {
                if (_showOfficer == null)
                {
                    _showOfficer = new RelayCommand<Officer>(new Action<Officer>(DisplayOfficer));
                }

                return _showOfficer;
            }
        }

        private RelayCommand<CivilianVeh> _showVehicle;
        public ICommand ShowVehicle
        {
            get
            {
                if (_showVehicle == null)
                {
                    _showVehicle = new RelayCommand<CivilianVeh>(new Action<CivilianVeh>(DisplayVehicle));
                }

                return _showVehicle;
            }
        }

        private RelayCommand<Civilian> _show911;
        public ICommand Show911
        {
            get
            {
                if (_show911 == null)
                {
                    //_show911 = new RelayCommand<EmergencyCall>(new Action<EmergencyCall>());
                }

                return _show911;
            }
        }
        #endregion

        #region Methods
        public DispatchViewModel(IMainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            Officers = new ObservableCollection<Officer>();
            Assignments = new ObservableCollection<Assignment>();
            Civilians = new ObservableCollection<Civilian>();
            Vehicles = new ObservableCollection<CivilianVeh>();
            Bolos = new ObservableCollection<Bolo>();
            for(int i = 0; i < 25; i++)
            {
                Civilians.Add(Civilian.CreateRandomCivilian());
                Vehicles.Add(new CivilianVeh("stupid"));
                Vehicles[i].Owner = Civilians[i];
                Vehicles[i].Plate = Civilians[i].Last;
            }
        }

        internal void DisplayCivilian(Civilian civ)
        {
            _mainViewModel.CivilianVM.Civ = civ;
            _mainViewModel.ShowFlyout("civilian");
        }

        internal void DisplayOfficer(Officer ofc)
        {
            _mainViewModel.OfficerVM.Ofc = ofc;
            _mainViewModel.ShowFlyout("officer");
        }

        internal void DisplayVehicle(CivilianVeh veh)
        {
            Console.WriteLine($"Plate: {veh.Plate}");
            _mainViewModel.VehVM.Vehicle = veh;
            _mainViewModel.ShowFlyout("vehicle");
        }

        internal void DisplayBolo(Bolo bolo)
        {
            _mainViewModel.BoloVM.Bolo = bolo;
            _mainViewModel.ShowFlyout("bolo");
        }

        internal void DisplayAssignment(Assignment assign)
        {
            _mainViewModel.AssignVM.Assign = assign;
            _mainViewModel.ShowFlyout("assignment");
        }

        private void ExecuteSearch()
        {
            Civilians = new ObservableCollection<Civilian>(Civilians.Where(c => c.First.Contains(CivSearch) || c.Last.Contains(CivSearch)));
        }
        #endregion
    }

    interface IDispatch
    {
        ObservableCollection<Assignment> Assignments { get; set; }
        ObservableCollection<Bolo> Bolos { get; set; }
        ObservableCollection<Civilian> Civilians { get; set; }
        ObservableCollection<CivilianVeh> Vehicles { get; set; }
        ObservableCollection<Officer> Officers { get; set; }
        bool CivsBackedup { get; set; }
        bool VehsBackedup { get; set; }
    }
}
