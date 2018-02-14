using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Dispatch.Common
{
    /// <summary>
    /// Permission levels
    /// </summary>
    [Serializable]
    public enum Permission
    {
        /// <summary>
        /// Permission available to everyone
        /// </summary>
        Everyone,
        /// <summary>
        /// Permission available for specific people
        /// </summary>
        Specific,
        /// <summary>
        /// Permission available to nobody
        /// </summary>
        None
    }
    [Serializable]
    public sealed class Permissions
    {
        #region Singleton
        // Threadsafe lock for permission
        private static readonly object _lock = new object();
        // Instance of permissions object
        private static Permissions obj;
        // The data of the files for permissions
        private static string _data;
        // Void to set the information listed above
        public static void SetInformation(string data)
        {
            _data = data;
        }

        /// <summary>
        /// Property for returning the permissions
        /// </summary>
        public static Permissions Get
        {
            get
            {
                if (obj != null) return obj; // Checking if instance is already initiated
                lock (_lock) // thread locking when instance null
                    if (obj == null) // checking if current thread thinks it's still null
                        obj = new Permissions(_data); // creating instance when everything checks out
                return obj; // returns the instance
            }
        }
        #endregion

        /// <summary>
        /// The key to find when searching for Civilian perms
        /// </summary>
        public const string CIV_KEY = "civilian:";
        /// <summary>
        /// The key to find when searching for LEO perms
        /// </summary>
        public const string COP_KEY = "leo:";
        /// <summary>
        /// The key to find when searching for Dispatch perms
        /// </summary>
        public const string DISPATCH_KEY = "dispatcher:";

        // A list of specific permissions
        private readonly Dictionary<string,string> items = new Dictionary<string, string>();

        /// <summary>
        /// The permission for the Civilian
        /// </summary>
        public Permission CivilianPermission { get; private set; } = Permission.Specific; // default specific
        /// <summary>
        /// The permission for the LEO
        /// </summary>
        public Permission LeoPermission { get; private set; } = Permission.Specific; // default specific
        /// <summary>
        /// The permission for the Dispatcher
        /// </summary>
        public Permission DispatchPermission { get; private set; } = Permission.Specific;

        /// <summary>
        /// License for Civilian specific permissions
        /// </summary>
        public IEnumerable<string> CivilianData
        {
            get
            {
                foreach (var item in items)
                    if (item.Value == CIV_KEY) // finding where they key is the civ key
                        //if (IPAddress.TryParse(item.Value, out IPAddress address)) // checking if the ip can be parsable
                            yield return item.Key; // yield returns the ip to a enumerable
            }
        }
        /// <summary>
        /// License for the LEO specific permissions
        /// </summary>
        public IEnumerable<string> LeoData
        {
            get
            {
                foreach (var item in items)
                    if (item.Value == COP_KEY) // finding where they key is the leo key
                        //if (IPAddress.TryParse(item.Value, out IPAddress address)) // checking if the ip can be parsable
                            yield return item.Key; // yield returns the ip to a enumerable
            }
        }
        /// <summary>
        /// License for the Dispatch specific permissions
        /// </summary>
        public IEnumerable<IPAddress> DispatchData
        {
            get
            {
                foreach (var item in items)
                    if (item.Value == DISPATCH_KEY)
                    { // finding where they key is the dispatch key
                        if (IPAddress.TryParse(item.Key, out IPAddress address))
                        { // checking if the ip can be parsable
                            // yield returns the ip to a enumerable
                        }
                        else
                        {
                            IPHostEntry host;
                            host = Dns.GetHostEntry(item.Key);
                            address = host.AddressList[0];
                        }
                        yield return address;
                    }

            }
        }

        #region constructor
        private Permissions(string fileData)
        {
            // setting file items
            Refresh(fileData);
        }
        public void Refresh(string data)
        {
            string current = string.Empty; // current key that the permissions is on
            // splitting the lines so that it is only important lines
            string[] lines = data.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).Where(x => !x.StartsWith("//"))
                .Select(x => x.Trim().ToLower()).ToArray();

            // reading each line in all of the important lines
            foreach (string line in lines)
            {
                // checking for key lines, then continuing
                if (line == CIV_KEY || line == COP_KEY || line == DISPATCH_KEY) { current = line; continue; }
                // if there is no key so far, then continue
                if (current == string.Empty) continue;

                // switch for the current line
                switch (line)
                {
                    // checking for permission type of everyone
                    case "everyone":
                        // checking on which key it is on
                        switch (current)
                        {
                            case CIV_KEY:
                                // setting the civilian permission as everyone
                                CivilianPermission = Permission.Everyone;
                                break;
                            case COP_KEY:
                                // setting the leo permission as everyone
                                LeoPermission = Permission.Everyone;
                                break;
                            case DISPATCH_KEY:
                                // setting the dispatch permission as everyone
                                DispatchPermission = Permission.Everyone;
                                break;
                        }
                        break;
                    // checking for permission type of none
                    case "none":
                        switch (current)
                        {
                            case CIV_KEY:
                                // setting the civilian permission as none
                                CivilianPermission = Permission.None;
                                break;
                            case COP_KEY:
                                // setting the leo permission as none
                                LeoPermission = Permission.None;
                                break;
                            case DISPATCH_KEY:
                                // setting the dispatch permission as none
                                DispatchPermission = Permission.None;
                                break;
                        }
                        break;
                    // if no conditions apply, add the line as an IP to items
                    default:
                        switch (current)
                        {
                            case CIV_KEY:
                                if (!CivContains(line))
                                {
                                    items.Add(line, current);
                                }
                                break;
                            case COP_KEY:
                                if (!LeoContains(line))
                                {
                                    items.Add(line, current);
                                }
                                break;
                            case DISPATCH_KEY:
                                if (!DispatchContains(IPAddress.Parse(line)))
                                {
                                    items.Add(line, current);
                                }
                                break;
                        }
                        
                        break;
                }
            }
        }
        #endregion

        // ez contains
        public bool CivContains(string lic) => CivilianData.Contains(lic);
        public bool LeoContains(string lic) => LeoData.Contains(lic);
        public bool DispatchContains(IPAddress address) => DispatchData.Contains(address);
    }
}
