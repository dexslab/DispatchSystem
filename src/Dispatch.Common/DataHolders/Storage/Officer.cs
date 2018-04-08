using System;
using System.Collections.Generic;

namespace Dispatch.Common.DataHolders.Storage
{
    [Serializable]
    public enum OfficerStatus
    {
        OnDuty,
        OffDuty,
        Busy
    }
    [Serializable]
    public class Officer : PlayerBase
    {
        public static Dictionary<string, OfficerStatus> StatusLookup = new Dictionary<string, OfficerStatus>()
        {
            { "OnDuty", OfficerStatus.OffDuty },
            { "OffDuty", OfficerStatus.OffDuty },
            { "Busy", OfficerStatus.Busy }
        };

        public string Callsign { get; set; }
        public OfficerStatus Status { get; set; }

        public Officer(string lic, string callsign) : base(lic)
        {
            Callsign = callsign;
            Status = OfficerStatus.OffDuty;
        }

        public override EventArgument[] ToArray()
        {
            return new EventArgument[]
            {
                Callsign,
                Status,
                new EventArgument[] {Id.ToString(), License, Creation.Ticks}
            };
        }
    }
}
