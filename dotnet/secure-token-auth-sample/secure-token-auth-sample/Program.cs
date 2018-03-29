using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace secure_token_auth_sample
{
    public class Program
    {
        private static readonly string ApiKey = "[YOUR_API_KEY]";
        private static readonly int ClientId = 0; //"[YOUR_CLIENT_ID]";
        private static readonly string Scope = "[SCOPE]"; //'readonly','playback',or 'all'
        private static readonly Encoding encoding = Encoding.UTF8;

        private static void Main(string[] args)
        {
            var ws = new WebServer(SendResponse, "http://localhost:8080/token/");
            ws.Run();
            Console.WriteLine("A simple webserver. Press a key to quit.");
            Console.ReadKey();
            ws.Stop();
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            const char separator = ':';
            var dto = new DateTimeOffset(DateTime.Now);
            var timestamp = dto.ToUnixTimeMilliseconds();
            var token = "";
            var keyByte = encoding.GetBytes(ApiKey);

            using (var hmacmd5 = new HMACMD5(keyByte))
            {
                hmacmd5.ComputeHash(encoding.GetBytes(ApiKey + separator + Scope + separator + timestamp));
                token = ByteToString(hmacmd5.Hash);
            }

            var json = new JavaScriptSerializer().Serialize(new
            {
                secure_token = token,
                timestamp = timestamp,
                client_id = ClientId
            });

            return json;
        }

        private static string ByteToString(byte[] buff)
        {
            var sbinary = "";
            for (int i = 0; i < buff.Length; i++)
                sbinary += buff[i].ToString("X2"); /* hex format */

            return sbinary;
        }

        public readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);

        private int GetUnixTimestamp(DateTime dt)
        {
            var span = dt - UnixEpoch;

            return (int)span.TotalSeconds;
        }
    }
}
