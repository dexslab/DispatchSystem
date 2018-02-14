﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch.Common.DataHolders.Storage
{
    [Serializable]
    public class Bolo : IDataHolder, IOwnable
    {
        string _player;
        string _reason;
        DateTime _creation;

        public string License { get; }
        public string Player => _player;
        public string Reason => _reason;
        public DateTime Creation => _creation;
        public BareGuid Id { get; }

        public Bolo(string playerName, string createrLic, string reason)
        {
            _player = playerName;
            License = string.IsNullOrWhiteSpace(createrLic) ? string.Empty : createrLic;
            _reason = reason;
            _creation = DateTime.Now;
            Id = BareGuid.NewBareGuid();
        }

        public object[] ToObjectArray()
        {
            return new[] { (object)_player, (object)_reason };
        }
    }
}
