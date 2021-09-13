
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Ksherpay
{
    public class MyUtil
	{
        internal static async Task<string> HttpContent(string method, string url, object payloadObj)
        {
            var payload = JsonConvert.SerializeObject(payloadObj);

            HttpClient httpClient = new HttpClient();
            HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

            HttpMethod httpmethord = new HttpMethod(method);
            HttpRequestMessage request = new HttpRequestMessage(httpmethord, url);
            request.Content = httpContent;

            var httpResponse = await httpClient.SendAsync(request);


            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                return responseContent;
            }
            return string.Empty;
        }

        public static void logDictionary(IDictionary<string, string> parameters)
        {
            Console.WriteLine("{");
            foreach (KeyValuePair<string, string> kvp in parameters)
            {
                Console.WriteLine("\"{0}\": \"{1}\",", kvp.Key, kvp.Value);
            }
            Console.WriteLine("}");
        }

        public static string GenerateTimestamp()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmss");

        }
    }
}
