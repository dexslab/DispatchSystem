using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Terminal.Utils
{
    class FiveMLicense
    {
        public static async Task<string> GetFiveMLicense()
        {
            ulong steamid = 0x210000100000000;
            Byte[] de = File.ReadAllBytes($"{Environment.GetEnvironmentVariable("LOCALAPPDATA")}\\DigitalEntitlements\\38d8f400-aa8a-4784-a9f0-26a08628577e");
            Byte[] decrypted = DPAPI.Decrypt(de, null, out string des);
            
            JObject obj = JObject.Parse(Encoding.Default.GetString(decrypted));
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "token", obj["guid"].ToString() },
                { "server", "127.0.0.1" },
                {"guid", steamid.ToString() }
            };
            foreach (string s in values.Keys)
            {
                Console.WriteLine($"{s}:{values[s]}");
            }
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://lambda.fivem.net/api/ticket/create", content);

            var responseString = await response.Content.ReadAsStringAsync();
            JObject tick = JObject.Parse(responseString);


            byte[] data = Convert.FromBase64String(tick["ticket"].ToString());

            if (data.Length < 20 + 4 + 128 + 4)
            {
                return null;
            }

            int length = (int)data[20 + 4 + 128];

            if (data.Length < 20 + 4 + 128 + 4 + length)
            {
                return null;
            }

            List<int> extraData = new List<int>();

            for (var index = 20 + 4 + 128 + 4; index < length + 20 + 4 + 128 + 4; index++)
            {
                extraData.Add(data[index]);
            }
            StringBuilder sb = new StringBuilder();
            foreach (int i in extraData)
            {
                string bufstring = String.Format("{0:X2}", i);
                sb.Append(bufstring);
            }

            return sb.ToString().ToLower();
        }
    }
}
