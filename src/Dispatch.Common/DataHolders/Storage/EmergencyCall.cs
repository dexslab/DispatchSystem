using System;
using System.Collections.Generic;

namespace Dispatch.Common.DataHolders.Storage
{
    [Serializable]
    public class EmergencyCall : IOwnable, IDataHolder
    {
        public EmergencyCall(string lic, string playerName)
        {
            Id = BareGuid.NewBareGuid();
            License = lic;
            Creation = DateTime.Now;
            PlayerName = playerName;
        }

        public bool Accepted { get; set; }
        public string PlayerName { get; }

        public string License { get; }
        public DateTime Creation { get; }
        public BareGuid Id { get; }
    }
}