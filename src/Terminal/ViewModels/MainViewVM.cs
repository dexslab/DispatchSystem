
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Terminal.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.Controls.Dialogs;
using WebSocketSharp;

namespace Terminal.ViewModels
{
    class MainViewVM : ViewModelBase, IMainViewModel
    {
        #region Variables
        private IDialogCoordinator dialogCoordinator;
        private MainView _manager;
        ObservableCollection<ViewModelBase> _pageViewModels;
        private string _title;
        RelayCommand<ViewModelBase> _changePageCommand;
        ViewModelBase _currentPageViewModel;
        LoginViewModel _loginViewModel;
        DispatchViewModel _dispatchVM;
        _911ViewModel _vM911;
        AssignmentViewModel _assignVM;
        BoloViewModel _boloVM;
        CivilianViewModel _civVM;
        OfficerViewModel _officerVM;
        VehicleViewModel _vehVM;
        WebSocket _socket;
        IDispatch _dispatch;
        #endregion

        #region Properties / Commands
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        private string _steamId;
        public string SteamId
        {
            get
            {
                return _steamId;
            }

            set
            {
                _steamId = value;
                if (!String.IsNullOrEmpty(SteamId))
                {
                    ShowSteam = true;
                }
                else
                {
                    ShowSteam = false;
                }
                RaisePropertyChanged();
            }
        }

        private string _license;
        public string License
        {
            get
            {
                return _license;
            }

            set
            {
                _license = value;
                if (!String.IsNullOrEmpty(License))
                {
                    ShowLic = true;
                }
                else
                {
                    ShowLic = false;
                }
                RaisePropertyChanged();
            }
        }

        private bool _showLic;
        public bool ShowLic
        {
            get
            {
                return _showLic;
            }
            set
            {
                _showLic = value;
                RaisePropertyChanged();
            }
        }

        private bool _showSteam;
        public bool ShowSteam
        {
            get
            {
                return _showSteam;
            }
            set
            {
                _showSteam = value;
                RaisePropertyChanged();
            }
        }

        private bool _show911;
        public bool Show911
        {
            get
            {
                return _show911;
            }
            set
            {
                _show911 = value;
                RaisePropertyChanged();
            }
        }

        private bool _showAssignment;
        public bool ShowAssignment
        {
            get
            {
                return _showAssignment;
            }
            set
            {
                _showAssignment = value;
                RaisePropertyChanged();
            }
        }

        private bool _showBolo;
        public bool ShowBolo
        {
            get
            {
                return _showBolo;
            }
            set
            {
                _showBolo = value;
                RaisePropertyChanged();
            }
        }

        private bool _showCivilian;
        public bool ShowCivilian
        {
            get
            {
                return _showCivilian;
            }
            set
            {
                _showCivilian = value;
                RaisePropertyChanged();
            }
        }

        private bool _showOfficer;
        public bool ShowOfficer
        {
            get
            {
                return _showOfficer;
            }
            set
            {
                _showOfficer = value;
                RaisePropertyChanged();
            }
        }

        private bool _showVehicle;
        public bool ShowVehicle
        {
            get
            {
                return _showVehicle;
            }
            set
            {
                _showVehicle = value;
                RaisePropertyChanged();
            }
        }



        public WebSocket Socket
        {
            get { return _socket; }

            set
            {
                _socket = value;
                RaisePropertyChanged();
            }
        }




        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand<ViewModelBase>(new Action<ViewModelBase>(ChangeViewModel));
                }

                return _changePageCommand;
            }
        }

        public ObservableCollection<ViewModelBase> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new ObservableCollection<ViewModelBase>();

                return _pageViewModels;
            }
            set
            {
                _pageViewModels = value;
                RaisePropertyChanged("PageViewModels");
            }
        }

        public ViewModelBase CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    RaisePropertyChanged("CurrentPageViewModel");
                }
            }
        }

        MainViewVM IMainViewModel.MainVM => this;








        #endregion

        #region Interface Stuffz
        public IDialogCoordinator DialogCoord => dialogCoordinator;
        public IDispatch DispatchCoord => _dispatch;
        public _911ViewModel _911VM
        {
            get { return _vM911; }
            set
            {
                _vM911 = value;
                RaisePropertyChanged();
            }
        }
        public AssignmentViewModel AssignVM
        {
            get { return _assignVM; }
            set
            {
                _assignVM = value;
                RaisePropertyChanged();
            }
        }
        public CivilianViewModel CivilianVM
        {
            get { return _civVM; }
            set
            {
                _civVM = value;
                RaisePropertyChanged();
            }
        }
        public OfficerViewModel OfficerVM
        {
            get { return _officerVM; }
            set
            {
                _officerVM = value;
                RaisePropertyChanged();
            }
        }
        public VehicleViewModel VehVM
        {
            get { return _vehVM; }
            set
            {
                _vehVM = value;
                RaisePropertyChanged();
            }
        }

        public BoloViewModel BoloVM
        {
            get { return _boloVM; }
            set
            {
                _boloVM = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Methods
        internal MainViewVM(MainView pFM, IDialogCoordinator instance)
        {
            _manager = pFM;
            Title = "Dispatch System Terminal";
            dialogCoordinator = instance;
            _loginViewModel = new LoginViewModel(this);
            PageViewModels.Add(_loginViewModel);
            CurrentPageViewModel = PageViewModels[0];
        }

        public void ShowFlyout(string v)
        {
            switch (v)
            {
                case "911":
                    Show911 = true;
                    break;
                case "assignment":
                    ShowAssignment = true;
                    break;
                case "civilian":
                    ShowCivilian = true;
                    break;
                case "bolo":
                    ShowBolo = true;
                    break;
                case "officer":
                    ShowOfficer = true;
                    break;
                case "vehicle":
                    ShowVehicle = true;
                    break;
            }
        }

        public void LoginAuthenticated()
        {
            _dispatchVM = new DispatchViewModel(this);
            PageViewModels.Add(_dispatchVM);
            _dispatch = _dispatchVM;
            _911VM = new _911ViewModel();
            AssignVM = new AssignmentViewModel();
            BoloVM = new BoloViewModel();
            CivilianVM = new CivilianViewModel();
            OfficerVM = new OfficerViewModel();
            VehVM = new VehicleViewModel();
        }

        public void ChangePage(string page)
        {
            switch (page)
            {
                case "Login":
                    ChangeViewModel(_loginViewModel);
                    break;
                case "Dispatch":
                    ChangeViewModel(_dispatchVM);
                    break;
            }
        }

        public void ChangeViewModel(ViewModelBase viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }
        #endregion

    }

    interface IMainViewModel
    {
        void ChangePage(string page);
        void LoginAuthenticated();
        void ShowFlyout(string v);
        string SteamId { get; set; }
        string License { get; set; }
        WebSocket Socket { get; set; }
        IDialogCoordinator DialogCoord { get; }
        MainViewVM MainVM { get; }
        bool ShowLic { get; set; }
        bool ShowSteam { get; set; }
        IDispatch DispatchCoord { get; }
        _911ViewModel _911VM { get; }
        AssignmentViewModel AssignVM { get; set; }
        BoloViewModel BoloVM { get; set; }
        CivilianViewModel CivilianVM { get; set; }
        OfficerViewModel OfficerVM { get; set; }
        VehicleViewModel VehVM { get; set; }
    }
}
