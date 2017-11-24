using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace APIClientTool.Utilities
{
    public class APIGenerateAuthHeader
    {
        static string emailAddress = string.Empty;
        static string userToken = string.Empty;

        #region Generate Headers with Hashed Signature 

        #region Generate Auth Header
        public static void GenerateAuthHeader(PublicAPIClient client, string requestUri, string methodType)
        {
            userToken = Utility.GetAppSettings("UserToken");
            string apiPrivateKey = Utility.GetAppSettings("PrivateKey");
            string apiPublicKey = Utility.GetAppSettings("PublicKey");

            client.DefaultRequestHeaders.Clear();

            if (!string.IsNullOrWhiteSpace(userToken))
            {
                string authorizeHeader = !string.IsNullOrWhiteSpace(userToken) ? userToken : string.Empty;
                client.DefaultRequestHeaders.Add("UserAuthorization", authorizeHeader);
            }

            var utcDate = DateTime.Now.ToUniversalTime().ToString("MM/dd/yyyy hh:mm:ss.fff tt");
            string utcDateString = string.Format("{0:U}", utcDate); //"Thursday, May 21, 2015 4:33:50 AM"

            client.DefaultRequestHeaders.Add("Timestamp", utcDateString);
            string authenticationHeader = apiPublicKey + ":" + ComputeHash(apiPrivateKey, BuildAuthSignature(methodType, utcDateString, requestUri.ToLower()));
            client.DefaultRequestHeaders.Add("Authentication", authenticationHeader);

        }
        #endregion

        public static string BuildAuthSignature(string methodType, string date, string absolutePath)
        {
            string message = string.Empty;
            absolutePath = "/" + absolutePath;
            var uri = HttpContext.Current.Server.UrlDecode(absolutePath);
            return string.Join("\n", methodType, date, uri);//, parameterMessage
        }

        #region Compute Hash
        private static string ComputeHash(string privateKey, string message)
        {
            var key = Encoding.UTF8.GetBytes(privateKey);
            string hashString;

            using (var hmac = new HMACSHA256(key))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                hashString = Convert.ToBase64String(hash);
            }

            return hashString;
        }

        #endregion

        #endregion
    }
}