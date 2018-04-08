using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatch.Common.DataHolders.Storage
{
    public static class Extensions
    {
        public static string ToJson(this Assignment p)
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.WriteStartObject();

            writer.WritePropertyName("id");
            writer.WriteValue(p.Id.ToString());
            writer.WritePropertyName("summary");
            writer.WriteValue(p.Summary);
            writer.WritePropertyName("created");
            writer.WriteValue(p.Creation.ToShortDateString());
            writer.WriteEndObject();

            return sw.ToString();
        }

        public static string ToJson(this Bolo p)
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.WriteStartObject();

            writer.WritePropertyName("id");
            writer.WriteValue(p.Id.ToString());
            writer.WritePropertyName("player");
            writer.WriteValue(p.Player);
            writer.WritePropertyName("license");
            writer.WriteValue(p.License);
            writer.WritePropertyName("reason");
            writer.WriteValue(p.Reason);
            writer.WritePropertyName("created");
            writer.WriteValue(p.Creation.ToShortDateString());
            writer.WriteEndObject();

            return sw.ToString();
        }

        

        public static string ToJson(this Civilian p)
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            // {
            writer.WriteStartObject();
            writer.WritePropertyName("id");
            writer.WriteValue(p.Id.ToString());
            writer.WritePropertyName("first");
            writer.WriteValue(p.First);
            writer.WritePropertyName("last");
            writer.WriteValue(p.Last);
            writer.WritePropertyName("license");
            writer.WriteValue(p.License);
            writer.WritePropertyName("warrants");
            writer.WriteValue(p.WarrantStatus);
            writer.WritePropertyName("citations");
            writer.WriteValue(p.CitationCount);
            writer.WritePropertyName("Notes");
            writer.WriteStartArray();
            foreach (string note in p.Notes)
            {
                writer.WriteValue(note);
            }
            writer.WriteEnd();
            writer.WritePropertyName("Tickets");
            writer.WriteStartArray();
            foreach (Ticket ticket in p.Tickets)
            {
                writer.WriteValue(ticket.ToJson());
            }
            writer.WriteEnd();
            writer.WritePropertyName("creation");
            writer.WriteValue(p.Creation.ToShortDateString());
            // }
            writer.WriteEndObject();

            return sw.ToString();
        }

        public static string ToJson(this CivilianVeh p)
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            // {
            writer.WriteStartObject();

            writer.WritePropertyName("civilian");
            writer.WriteValue(p.Owner.ToJson());
            writer.WritePropertyName("plate");
            writer.WriteValue(p.Plate);
            writer.WritePropertyName("stolen");
            writer.WriteValue(p.StolenStatus);
            writer.WritePropertyName("registered");
            writer.WriteValue(p.Registered);
            writer.WritePropertyName("insured");
            writer.WriteValue(p.Insured);
            // }
            writer.WriteEndObject();

            return sw.ToString();
        }

        public static string ToJson(this EmergencyCall p)
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            // {
            writer.WriteStartObject();

            writer.WritePropertyName("id");
            writer.WriteValue(p.Id.ToString());
            writer.WritePropertyName("license");
            writer.WriteValue(p.License);
            writer.WritePropertyName("player");
            writer.WriteValue(p.PlayerName);
            writer.WritePropertyName("accepted");
            writer.WriteValue(p.Accepted);
            writer.WritePropertyName("creation");
            writer.WriteValue(p.Creation.ToShortDateString());
            // }
            writer.WriteEndObject();

            return sw.ToString();
        }

        public static string ToJson(this Officer p)
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            // {
            writer.WriteStartObject();
            writer.WritePropertyName("id");
            writer.WriteValue(p.Id.ToString());
            writer.WritePropertyName("license");
            writer.WriteValue(p.License);
            writer.WritePropertyName("callsign");
            writer.WriteValue(p.Callsign);
            writer.WritePropertyName("status");
            writer.WriteValue(p.Status);
            writer.WritePropertyName("creation");
            writer.WriteValue(p.Creation.ToShortDateString());

            // }
            writer.WriteEndObject();

            return sw.ToString();
        }

        public static string ToJson(this Ticket p)
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            // {
            writer.WriteStartObject();
            writer.WritePropertyName("id");
            writer.WriteValue(p.Id.ToString());
            writer.WritePropertyName("amount");
            writer.WriteValue(p.Amount);
            writer.WritePropertyName("reason");
            writer.WriteValue(p.Reason);
            writer.WritePropertyName("Paid");
            //writer.WriteValue(p.Paid);
            //writer.WritePropertyName("DatePaid");
            //writer.WriteValue(p.DatePaid.ToShortDateString());
            //writer.WritePropertyName("WarrantIssued");
            //writer.WriteValue(p.WarrantIssued);
            //writer.WritePropertyName("WarrantServed");
            //writer.WriteValue(p.WarrantServed);
            writer.WritePropertyName("creation");
            writer.WriteValue(p.Creation.ToShortDateString());
            // }
            writer.WriteEndObject();

            return sw.ToString();
        }
    }
}
