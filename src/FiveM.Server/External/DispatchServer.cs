using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Config.Reader;

using Dispatch.Common.DataHolders.Storage;


using CitizenFX.Core;
using Fleck;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using Dispatch.Common;
using DispatchSystem.Server.Main;


namespace DispatchSystem.Server.External
{
    public class DispatchServer
    {
        private WebSocketServer server; // server from CloNET
        private readonly string ip; // ip of the server
        private readonly int port; // port of the server

        /// <summary>
        /// <see cref="Array"/> of <see cref="ConnectedPeer"/> that are currently connected to the server
        /// </summary>
        public static Dictionary<string, IWebSocketConnection> ConnectedDispatchers = new Dictionary<string, IWebSocketConnection>();

        // 911calls items, for keeping track of dispatchers connected to which 911 call
        internal Dictionary<BareGuid, string> Calls;

        /// <summary>
        /// Initializes the <see cref="DispatchSystem"/> class from a config
        /// </summary>
        /// <param name="cfg"></param>
        internal DispatchServer(ServerConfig cfg)
        {
            Calls = new Dictionary<BareGuid, string>(); // creating the calls item
            ip = cfg.GetStringValue("server", "ip", "0.0.0.0"); // setting the ip
            Log.WriteLine("Setting ip to " + ip);
            port = cfg.GetIntValue("server", "port", 33333); // setting the port
            Log.WriteLine("Setting port to " + port);
            Start(); // starting the server
        }

        private void Start()
        {
            Log.WriteLine("Creating TCP Device");
            var server = new WebSocketServer($"ws://{ip}:{port}");
            server.Start(socket =>
            {
                socket.OnOpen = () => OnConnect(socket);
                socket.OnClose = () => Log.WriteLine("Close!");
                socket.OnMessage = message => MessageReceived(socket, message);
            });

        }

        private async void MessageReceived(IWebSocketConnection client, string message)
        {
            JObject parsedMessage = JObject.Parse(message);
            JObject obj;
            JArray array;
            try
            {
                switch ((string)parsedMessage["message"])
                {
                    case "LoginDispatcher":
                        Log.WriteLine($"Logging in dispatcher with id of {(string)parsedMessage["id"]}");
                        obj = new JObject();
                        obj.Add("message", "login");
                        if (CheckPerms((string)parsedMessage["id"]))
                        {
                            obj.Add(new JProperty("permission", "granted"));
                            await client.Send(obj.ToString());
                        }
                        else
                        {
                            obj.Add(new JProperty("permission", "denied"));
                            await client.Send(obj.ToString());
                        }
                        break;
                    case "GetCivilian":
                        obj = new JObject
                        {
                            { "message", "civilian" },
                            { "civilian", GetCivilian((string)parsedMessage["first"], (string)parsedMessage["last"]).ToJson() }
                        };
                        await client.Send(obj.ToString());
                        break;
                    case "GetCivilianVeh":
                        obj = new JObject
                        {
                            {"message", "vehicle" },
                            {"vehicle", GetCivilianVeh((string)parsedMessage["plate"]).ToJson() }
                        };
                        await client.Send(obj.ToString());
                        break;
                    case "GetOfficer":
                        obj = new JObject
                        {
                            {"message", "officer" },
                            {"officer", GetOfficer(BareGuid.Parse((string)parsedMessage["id"])).ToJson() }
                        };
                        await client.Send(obj.ToString());
                        break;
                    case "CreateAssignment":
                        obj = new JObject
                        {
                            {"message","newassign"},
                            {"id", NewAssignment((string)parsedMessage["summary"]).ToString() }
                        };
                        await client.Send(obj.ToString());
                        break;
                    case "GetOfficerAssignment":
                        obj = new JObject
                        {
                            {"message","ofcassign"},
                            {"ofcassign",  GetOfcAssignment(BareGuid.Parse((string)parsedMessage["id"])).ToJson()}
                        };
                        await client.Send(obj.ToString());
                        break;
                    case "Accept911":
                        obj = new JObject
                        {
                            {"message","911accepted"},
                            {"accepted", AcceptEmergency(BareGuid.Parse((string)parsedMessage["id"]))}
                        };
                        await client.Send(obj.ToString());
                        break;
                    case "Bolos":
                        StorageManager<Bolo> bolos = await GetBolos();
                        obj = new JObject();
                        obj.Add("message", "bolos");
                        for (int i = 0; i < bolos.Count; i++)
                        {
                            obj.Add(new JProperty(bolos[i].ToJson()));
                        }
                        await client.Send(obj.ToString());
                        break;
                    case "Officers":
                        StorageManager<Officer> officers = await GetOfficers();
                        obj = new JObject();
                        obj.Add("message", "officers");
                        array = new JArray();
                        for (int i = 0; i < officers.Count; i++)
                        {
                            array.Add(officers[i].ToJson());
                        }
                        obj.Add("officers", array);
                        await client.Send(obj.ToString());
                        break;
                    case "Assignments":
                        StorageManager<Assignment> assignments = await GetAssignments();
                        obj = new JObject();
                        obj.Add("message", "assignments");
                        array = new JArray();
                        for (int i = 0; i < assignments.Count; i++)
                        {
                            array.Add(assignments[i].ToJson());
                        }
                        obj.Add("assignments", array);
                        await client.Send(obj.ToString());
                        break;
                    case "Vehicles":
                        StorageManager<CivilianVeh> vehicles = await GetVehicles();
                        obj = new JObject();
                        obj.Add("message", "vehicles");
                        array = new JArray();
                        for (int i = 0; i < vehicles.Count; i++)
                        {
                            array.Add(vehicles[i].ToJson());
                        }
                        obj.Add("vehicles", array);
                        await client.Send(obj.ToString());
                        break;
                    case "Civilians":
                        StorageManager<Civilian> civilians = await GetCivilians();
                        obj = new JObject();
                        obj.Add("message", "civilians");
                        array = new JArray();
                        for (int i = 0; i < civilians.Count; i++)
                        {
                            array.Add(civilians[i].ToJson());
                        }
                        obj.Add("civilians", array);
                        await client.Send(obj.ToString());
                        break;
                    case "911End":
                        obj = new JObject
                        {
                            {"message","911ended"},
                        };
                        await client.Send(obj.ToString());
                        break;
                    case "911Msg":
                        obj = new JObject
                        {
                            {"message","911msgsent"}
                        };
                        await MessageEmergency(BareGuid.Parse((string)parsedMessage["id"]), (string)parsedMessage["msg"]);
                        await client.Send(obj.ToString());
                        break;
                    case "AddOfficerAssignment":
                        obj = new JObject
                        {
                            {"message","addedofcassignment"}
                        };
                        await AddOfcAssignment(BareGuid.Parse((string)parsedMessage["id"]), BareGuid.Parse((string)parsedMessage["ofcid"]));
                        await client.Send(obj.ToString());
                        break;
                    case "RemoveAssignment":
                        obj = new JObject
                        {
                            {"message","removedassignment"}
                        };
                        await RemoveAssignment(BareGuid.Parse((string)parsedMessage["id"]));
                        await client.Send(obj.ToString());
                        break;
                    case "RemoveOfficerAssignment":
                        obj = new JObject
                        {
                            {"message","removedofcassignment"}
                        };
                        await RemoveOfcAssignment(BareGuid.Parse((string)parsedMessage["ofcid"]));
                        await client.Send(obj.ToString());
                        break;
                    case "SetStatus":
                        obj = new JObject
                        {
                            {"message","statusset"}
                        };
                        await ChangeOfficerStatus(BareGuid.Parse((string)parsedMessage["ofcid"]), Officer.StatusLookup[parsedMessage["ofcid"].ToString()]);
                        await client.Send(obj.ToString());
                        break;
                    case "RemoveOfficer":
                        obj = new JObject
                        {
                            {"message","officerremoved"},
                        };
                        await RemoveOfficer(BareGuid.Parse((string)parsedMessage["ofcid"]));
                        await client.Send(obj.ToString());
                        break;
                    case "AddBolo":
                        obj = new JObject
                        {
                            {"message","boloadded"}
                        };
                        await AddBolo((string)parsedMessage["player"], (string)parsedMessage["bolo"]);
                        await client.Send(obj.ToString());
                        break;
                    case "RemoveBolo":
                        obj = new JObject
                        {
                            {"message","boloremoved"}
                        };
                        await RemoveBolo((int)parsedMessage["index"]);
                        await client.Send(obj.ToString());
                        break;
                    case "AddNote":
                        obj = new JObject
                        {
                            {"message","noteadded"},
                        };
                        await AddNote(BareGuid.Parse((string)parsedMessage["id"]), (string)parsedMessage["note"]);
                        await client.Send(obj.ToString());
                        break;
                }
            }
            catch (Exception e)
            {
                Log.WriteLine($"Got an error parsing message {parsedMessage[message]} due to {e.Message}");
            }
        }

        private async Task OnConnect(IWebSocketConnection connection)
        {
#if DEBUG
            Log.WriteLine($"[{connection.ConnectionInfo.ClientIpAddress}] Connected");
#else
                Log.WriteLineSilent($"[{connection.ConnectionInfo.ClientIpAddress}] Connected");
#endif
            // dispose if the permissions bad
            /*if (!CheckPerms(connection.ConnectionInfo.ClientIpAddress.ToString()))
            {
                JObject message = new JObject(new JProperty("message", "SendInvalidPerms"));
                await connection.Send(message.ToString());
                connection.Close();
            }
            else
            {
#if DEBUG
                Log.WriteLine($"{connection.ConnectionInfo.ClientIpAddress} is autherized enabling window");
#endif
                JObject message = new JObject(new JProperty("message", "enableUI"));
                await connection.Send(message.ToString());
                ConnectedDispatchers.Add(connection.ConnectionInfo.Id.ToString(), connection);
                return;
            }*/
        }

        private static async Task OnDisconnect()
        {
            await Task.Run(delegate
            {
                // logging the ip disconnected
#if DEBUG
                //Log.WriteLine($"[{user.RemoteIP}] Disconnected");
#else
                Log.WriteLineSilent($"[{user.RemoteIP}] Disconnected");
#endif
            });
        }

        /*
           _____              _        _        ____                _____   _  __   _____     
          / ____|     /\     | |      | |      |  _ \      /\      / ____| | |/ /  / ____|  _ 
         | |         /  \    | |      | |      | |_) |    /  \    | |      | ' /  | (___   (_)
         | |        / /\ \   | |      | |      |  _ <    / /\ \   | |      |  <    \___ \     
         | |____   / ____ \  | |____  | |____  | |_) |  / ____ \  | |____  | . \   ____) |  _ 
          \_____| /_/    \_\ |______| |______| |____/  /_/    \_\  \_____| |_|\_\ |_____/  (_)

        */

        private Civilian GetCivilian(string first, string last)
        {

            Civilian civ = Common.GetCivilianByName(first, last);

            return civ;
        }
        private CivilianVeh GetCivilianVeh(string plate)
        {

            CivilianVeh civVeh = Common.GetCivilianVehByPlate(plate);

            return civVeh;
        }
        private Officer GetOfficer(BareGuid id)
        {

            Officer ofc = Core.Officers.ToList().Find(x => x.Id == id);
            return ofc;
        }
        private Assignment GetOfcAssignment(BareGuid id)
        {

            Officer ofc = Core.Officers.ToList().Find(x => x.Id == id);
            return Common.GetOfficerAssignment(ofc);
        }
        private bool AcceptEmergency(BareGuid id)
        {
            EmergencyCall acceptedCall = Core.CurrentCalls.FirstOrDefault(x => x.Id == id);
            if (Calls.ContainsKey(id) || acceptedCall == null) return false;
            global::DispatchSystem.Server.Main.DispatchSystem.Invoke(delegate
            {
                Player p = Common.GetPlayerByLicense(acceptedCall.License);
                if (p != null)
                {
                    Common.SendMessage(p, "Dispatch911", new[] { 255, 0, 0 }, "Your 911 call has been accepted by a Dispatcher");
                }
            });
            return true;
        }
        private async Task<StorageManager<Bolo>> GetBolos()
        {
            await Task.FromResult(0);
            return Core.Bolos;
        }
        private async Task<StorageManager<Officer>> GetOfficers()
        {
            await Task.FromResult(0);
            return Core.Officers;
        }
        private async Task<StorageManager<Assignment>> GetAssignments()
        {
            await Task.FromResult(0);

            return Core.Assignments;
        }
        private async Task<StorageManager<CivilianVeh>> GetVehicles()
        {
            await Task.FromResult(0);
            return Core.CivilianVehs;
        }

        private async Task<StorageManager<Civilian>> GetCivilians()
        {
            await Task.FromResult(0);
            return Core.Civilians;
        }
        private async Task EndEmergency(BareGuid id)
        {
            await Task.FromResult(0);
            Calls.Remove(id); // removing the id from the calls

            EmergencyCall call = Core.CurrentCalls.FirstOrDefault(x => x.Id == id); // obtaining the call from the civ

            if (Core.CurrentCalls.Remove(call)) // remove, if successful, then notify
            {
                Main.DispatchSystem.Invoke(delegate
                {
                    Player p = Common.GetPlayerByLicense(call?.License);

                    if (p != null)
                    {
                        Common.SendMessage(p, "Dispatch911", new[] { 255, 0, 0 }, "Your 911 call was ended by a Dispatcher");
                    }
                });
            }
        }
        private async Task MessageEmergency(BareGuid id, string msg)
        {
            await Task.FromResult(0);
            EmergencyCall call = Core.CurrentCalls.FirstOrDefault(x => x.Id == id); // finding the call
            global::DispatchSystem.Server.Main.DispatchSystem.Invoke(() =>
            {
                Player p = Common.GetPlayerByLicense(call?.License); // getting the player from the call's ip

                if (p != null)
                {
                    Common.SendMessage(p, "Dispatcher", new[] { 0x0, 0xff, 0x0 }, msg);
                }
            });
        }
        private async Task AddOfcAssignment(BareGuid id, BareGuid ofcId)
        {
            await Task.FromResult(0);
            Assignment assignment = Core.Assignments.FirstOrDefault(x => x.Id == id); // finding assignment from the id
            Officer ofc = Core.Officers.ToList().Find(x => x.Id == id); // finding the officer from the id
            if (assignment is null || ofc is null) // returning if either is null
                return;
            if (Core.OfficerAssignments.ContainsKey(ofc)) // returning if the officer already contains the assignment
                return;

            Core.OfficerAssignments.Add(ofc, assignment); // adding the assignment to the officer

            ofc.Status = OfficerStatus.OffDuty;

            // notify of assignment
            Main.DispatchSystem.Invoke(() =>
            {
                Player p = Common.GetPlayerByLicense(ofc.License);
                if (p != null)
                    Common.SendMessage(p, "^8DispatchCAD", new[] { 0, 0, 0 }, $"New assignment added: \"{assignment.Summary}\"");
            });
        }
        private BareGuid NewAssignment(string summary)
        {
            try
            {
                Assignment assignment = new Assignment(summary);
                Core.Assignments.Add(assignment);
                return assignment.Id; // returning the assignment id
            }
            catch
            {
                return null;
            }
        }
        private async Task RemoveAssignment(BareGuid id)
        {
            await Task.FromResult(0);
            Assignment item2 = Core.Assignments.FirstOrDefault(x => x.Id == id); // finding the assignment from the id
            Common.RemoveAllInstancesOfAssignment(item2); // removing using common
        }
        private async Task RemoveOfcAssignment(BareGuid ofcId)
        {
            await Task.FromResult(0);
            Officer ofc = Core.Officers.FirstOrDefault(x => x.Id == ofcId);
            if (ofc == null) return;

            if (!Core.OfficerAssignments.ContainsKey(ofc)) return;
            Core.OfficerAssignments.Remove(ofc); // removing the assignment from the officer

            ofc.Status = OfficerStatus.OnDuty; // set on duty

            Main.DispatchSystem.Invoke(() =>
            {
                Player p = Common.GetPlayerByLicense(ofc.License);
                if (p != null)
                    Common.SendMessage(p, "^8DispatchCAD", new[] { 0, 0, 0 }, "Your assignment has been removed by a dispatcher");
            });
        }
        private async Task ChangeOfficerStatus(BareGuid id, OfficerStatus status)
        {
            await Task.FromResult(0);
            Officer ofc = Core.Officers.FirstOrDefault(x => x.Id == id);

            if (ofc is null) return; // checking for null

            if (ofc.Status != status)
            {
                ofc.Status = status; // changing the status
                Main.DispatchSystem.Invoke(() =>
                {
                    Player p = Common.GetPlayerByLicense(ofc.License);
                    if (p != null)
                        Common.SendMessage(p, "^8DispatchCAD", new[] { 0, 0, 0 },
                            $"Dispatcher set status to {(ofc.Status == OfficerStatus.OffDuty ? "Off Duty" : ofc.Status == OfficerStatus.OnDuty ? "On Duty" : "Busy")}");
                });
            }
            else
            {
            }
        }
        private async Task RemoveOfficer(BareGuid id)
        {
            await Task.FromResult(0);
            Officer ofc = Core.Officers.FirstOrDefault(x => x.Id == id);
            if (ofc != null)
            {
                // notify of removing of role
                Main.DispatchSystem.Invoke(delegate
                {
                    Player p = Common.GetPlayerByLicense(ofc.License);

                    if (p != null)
                        Common.SendMessage(p, "^8DispatchCAD", new[] { 0, 0, 0 }, "You have been removed from your officer role by a dispatcher");
                });

                // actually remove the officer from the list
                Core.Officers.Remove(ofc);
            }
            else
            {
            }
        }
        private async Task AddBolo(string player, string bolo)
        {
            await Task.FromResult(0);
            Core.Bolos.Add(new Bolo(player, string.Empty, bolo));
        }
        private async Task RemoveBolo(int parse)
        {
            await Task.FromResult(0);
            try
            {
                // removing at the specified index
                Core.Bolos.RemoveAt(parse);
            }
            // thrown when argument is out of range
            catch (ArgumentOutOfRangeException)
            {
            }
        }
        private async Task AddNote(BareGuid id, string note)
        {
            await Task.FromResult(0);
            Civilian civ = Core.Civilians.FirstOrDefault(x => x.Id == id); // finding the civ from the id

            if (civ != null)
            {
                civ.Notes.Add(note); // adding the note for the civilian
            }
            else
            {
            }
        }
        private bool CheckPerms(string sender)
        {
#if DEBUG
            foreach (string s in Core.DispatchPerms)
            {
                Log.WriteLine($"{s} in dispatchers");
            }
#endif
            bool check = false;
            if (Core.DispatchPerms.Contains("everyone"))
            {
                check = true;
            }
            if (!Core.DispatchPerms.Contains("none") && Core.DispatchPerms.Contains(sender))
            {
                check = true;
            }
            return check;
        }


        //private static bool _(string ip)
        //{
        //    switch (Permissions.Get.DispatchPermission)
        //    {
        //        case Permission.Specific: // checking for specific permissions
        //            if (!Permissions.Get.DispatchContains(IPAddress.Parse(ip))) // checking if the ip is in the perms
        //            {
        //                Log.WriteLine($"[{ip}] NOT ENOUGH DISPATCH PERMISSIONS"); // log if not
        //                return true;
        //            }
        //            break;
        //        case Permission.None:
        //            Log.WriteLine($"[{ip}] NOT ENOUGH DISPATCH PERMISSIONS");
        //            return true; // automatically return not
        //        case Permission.Everyone:
        //            break; // continue through
        //        default:
        //            throw new ArgumentOutOfRangeException(); // throw because there is no other options
        //    }
        //    return false; // return false by default
        //}
    }
}
