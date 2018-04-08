using System;

namespace Dispatch.Common.DataHolders.Storage
{
    [Serializable]
    public class Bolo : IDataHolder, IOwnable, IEventInfo
    {
        public string License { get; }
        public string Player { get; }
        public string Reason { get; }
        public DateTime Creation { get; }
        public BareGuid Id { get; }

        public Bolo(string playerName, string lic, string reason)
        {
            Player = playerName;
            License = string.IsNullOrWhiteSpace(lic) ? string.Empty : lic;
            Reason = reason;
            Creation = DateTime.Now;
            Id = BareGuid.NewBareGuid();
        }

        public EventArgument[] ToArray()
        {
            return new EventArgument[]
            {
                Player,
                Reason,
                new EventArgument[] {Id.ToString(), License, Creation.Ticks}
            };
        }
    }
}
