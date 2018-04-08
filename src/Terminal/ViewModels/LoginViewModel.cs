using GalaSoft.MvvmLight;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Utils;
using Steamworks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Threading;
using Dispatch.Common.DataHolders.Storage;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Terminal.ViewModels
{
    class LoginViewModel : ViewModelBase
    {

        #region Variables
        IMainViewModel _mainViewModel;
        private AuthMethod auth;

        string _serverIp;
        public string ServerIp
        {
            get { return _serverIp; }
            set
            {
                _serverIp = value;
                RaisePropertyChanged();
            }
        }

        int _serverPort;
        public int ServerPort
        {
            get { return _serverPort; }
            set
            {
                _serverPort = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand _loginViaFiveM;
        public ICommand LoginViaFiveM
        {
            get
            {
                if (_loginViaFiveM == null)
                {
                    _loginViaFiveM = new RelayCommand(new Action(FiveMLogin));
                }

                return _loginViaFiveM;
            }
        }

        private RelayCommand _loginViaSteam;
        public ICommand LoginViaSteam
        {
            get
            {
                if (_loginViaSteam == null)
                {
                    _loginViaSteam = new RelayCommand(new Action(SteamLogin));
                }

                return _loginViaSteam;
            }
        }
        #endregion


        public LoginViewModel(IMainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ServerIp = "192.168.1.4";
            ServerPort = 33333;
            GetIdentifiers();
        }

        async void GetIdentifiers()
        { 
            _mainViewModel.License = await FiveMLicense.GetFiveMLicense();
            Console.WriteLine("Trying steam");
            if (!SteamAPI.Init())
            {
                Console.WriteLine("NO STEAM!!!!!");
                return;
            }

            if (!Packsize.Test())
            {
                Console.WriteLine("You're using the wrong Steamworks.NET Assembly for this platform!");
                return;
            }

            if (!DllCheck.Test())
            {
                Console.WriteLine("You're using the wrong dlls for this platform!");
                return;
            }
            CSteamID steamId = SteamUser.GetSteamID();
            _mainViewModel.SteamId = string.Format("{0:X}", steamId.m_SteamID).ToLower();
            SteamAPI.Shutdown();
        }

        internal async void FiveMLogin()
        {
            auth = AuthMethod.FiveM;
            Connect();
        }

        internal async void SteamLogin()
        {
            auth = AuthMethod.Steam;
            Connect();
        }

        internal async void Connect()
        {
            _mainViewModel.Socket = new WebSocketSharp.WebSocket($"ws://{ServerIp}:{ServerPort}");
            _mainViewModel.Socket.OnOpen += Socket_OnOpen;
            _mainViewModel.Socket.OnMessage += Socket_OnMessage;
            _mainViewModel.Socket.ConnectAsync();
        }

        private void Socket_OnOpen(object sender, EventArgs e)
        {
            Console.WriteLine("We are connected Waiting to send message");
            JObject obj = new JObject();
            obj.Add("message", "LoginDispatcher");
            obj.Add("id", (auth == AuthMethod.FiveM) ? _mainViewModel.License : _mainViewModel.SteamId);
            _mainViewModel.Socket.Send(obj.ToString());
        }

        private void Socket_OnMessage(object sender, WebSocketSharp.MessageEventArgs e)
        {
            JObject obj = JObject.Parse(e.Data);
            switch ((string)obj["message"])
            {
                case "login":
                    ProcessPermissions((string)obj["permission"]);
                    break;
            }
        }

        private async void ProcessPermissions(string v)
        {
            if (v.Equals("granted"))
            {
                Console.WriteLine("Welcome to dispatch terminal");
                _mainViewModel.LoginAuthenticated();
                _mainViewModel.Socket.OnMessage -= Socket_OnMessage;
                _mainViewModel.Socket.OnMessage += DispatchOnMessage;
                _mainViewModel.ChangePage("Dispatch");
            }
            else
            {
               await _mainViewModel.DialogCoord.ShowMessageAsync(_mainViewModel.MainVM, "Invalid Permissions", "You do not have Dispatch permissions");
                Console.WriteLine("Closing app now");
            }
        }

        private void DispatchOnMessage(object sender, WebSocketSharp.MessageEventArgs e)
        {
            JObject obj = JObject.Parse(e.Data);
            switch ((string)obj["message"])
            {
                case "civilian":
                    _mainViewModel.CivilianVM.Civ = JsonConvert.DeserializeObject<Civilian>((string)obj["civilian"]);
                    _mainViewModel.ShowFlyout("Civilian");
                    break;
                case "vehicle":
                    JObject veh = JObject.FromObject(obj["vehicle"]);
                    break;
                case "officer":
                    break;
                case "newassign":
                    break;
                case "ofcassign":
                    break;
                case "911accepted":
                    break;
                case "bolos":
                    _mainViewModel.DispatchCoord.Bolos = JsonConvert.DeserializeObject<ObservableCollection<Bolo>>((string)obj["bolos"]);
                    break;
                case "officers":
                    _mainViewModel.DispatchCoord.Officers = JsonConvert.DeserializeObject<ObservableCollection<Officer>>((string)obj["officers"]);
                    break;
                case "assignments":
                    _mainViewModel.DispatchCoord.Assignments = JsonConvert.DeserializeObject<ObservableCollection<Assignment>>((string)obj["assignments"]);
                    break;
                case "vehicles":
                    _mainViewModel.DispatchCoord.VehsBackedup = false;
                    _mainViewModel.DispatchCoord.Vehicles = JsonConvert.DeserializeObject<ObservableCollection<CivilianVeh>>((string)obj["vehicles"]);
                    break;
                case "civilians":
                    _mainViewModel.DispatchCoord.CivsBackedup = false;
                    _mainViewModel.DispatchCoord.Civilians = JsonConvert.DeserializeObject<ObservableCollection<Civilian>>((string)obj["civilians"]);
                    break;
                case "911ended":
                    break;
                case "911msgsent":
                    break;
                case "addedofcassignment":
                    break;
                case "removedassignment":
                    break;
                case "removedofcassignment":
                    break;
                case "statusset":
                    break;
                case "officerremoved":
                    break;
                case "boloremoved":
                    break;
                case "noteadded":
                    break;
                case "911alert":
                    break;
                case "emsg":
                    break;
            }
        }

    }

    enum AuthMethod
    {
        FiveM,
        Steam
    }
}
