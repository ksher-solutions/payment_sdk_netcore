using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ksherpay
{
    class Ksherpay
    {
        private string base_url;
        private string apiType;
        private string token;
        private Provider provider;
        private string mid;
        private string endpoint;

        public Ksherpay(string base_url, string apiType, string token, Provider? provider=Provider.Ksher, string? mid=null, int? timeout= 10)
        {
            this.base_url = base_url;
            this.apiType = apiType;
            this.token = token;
            this.provider = (Provider)provider;
            this.mid = mid;

            if (apiType == ApiType.redirect)
            {
                endpoint = "/api/v1/redirect/orders";
            }
            else if (apiType == ApiType.cscanb)
            {
                endpoint = "/api/v1/cscanb/orders";
            }
            else
            {
                endpoint = "/api/v1";
            }
        }
        public IDictionary<string, string> create(IDictionary<string,string> parameters)
        {
            parameters.Add("timestamp", MyUtil.GenerateTimestamp());
            parameters.Add("signature", SignRequest(parameters, endpoint));
            return request("POST", endpoint,parameters);
        }
        public IDictionary<string, string> query(IDictionary<string, string> parameters)
        {
            string order_id = parameters["order_id"];
            parameters.Remove("order_id");

            parameters.Add("timestamp", MyUtil.GenerateTimestamp());
            parameters.Add("signature", SignRequest(parameters, endpoint+"/"+ order_id));
            return request("GET", endpoint + "/" + order_id, parameters);
        }

        public IDictionary<string, string> refund(IDictionary<string, string> parameters)
        {
            string order_id = parameters["order_id"];
            parameters.Remove("order_id");

            parameters.Add("timestamp", MyUtil.GenerateTimestamp());
            parameters.Add("signature", SignRequest(parameters, endpoint + "/" + order_id));
            return request("PUT", endpoint + "/" + order_id, parameters);
        }
        public IDictionary<string, string> cancle(IDictionary<string, string> parameters)
        {
            string order_id = parameters["order_id"];
            parameters.Remove("order_id");

            parameters.Add("timestamp", MyUtil.GenerateTimestamp());
            parameters.Add("signature", SignRequest(parameters, endpoint + "/" + order_id));
            return request("DELETE", endpoint + "/" + order_id, parameters);
        }

        private IDictionary<string, string> request(string method, string endpoint, IDictionary<string, string> parameters)
        {
            //Console.WriteLine("request:");
            //MyUtil.logDictionary(parameters);
            //Console.WriteLine("=============");

            var request = Task.Run(() => MyUtil.HttpContent(method, base_url + endpoint, parameters));
            request.Wait();

            try
            {
                var response = JsonConvert.DeserializeObject<IDictionary<string, string>>(request.Result);

                //Console.WriteLine("response:");
                //MyUtil.logDictionary(response);
                //Console.WriteLine("=============");

                if (response.ContainsKey("signature"))
                {
                    if (!checkSignature(response, endpoint))
                    {
                        IDictionary<string, string> error_response = new Dictionary<string, string>();
                        error_response.Add("force_clear", "False");
                        error_response.Add("cleared", "False");
                        error_response.Add("error_code", "VERIFY_KSHER_SIGN_FAIL");
                        error_response.Add("error_message", @"verify signature failed");
                        error_response.Add("locked", "False");
                        return error_response;
                    }
                }
                

                return response;
            }
            catch (Exception ex)
            {
                IDictionary<string, string> error_response = new Dictionary<string, string>();
                error_response.Add("force_clear", "False");
                error_response.Add("cleared", "False");
                error_response.Add("error_code", ex.ToString());
                error_response.Add("error_message", ex.ToString());
                error_response.Add("locked", "False");
                return error_response;
            }
        }
        
        public bool checkSignature(IDictionary<string, string> parameters, string endpoint)
        {
            string ori_signature=parameters["signature"];
            parameters.Remove("signature");
            parameters.Remove("log_entry_url");
            string signature = SignRequest(parameters, endpoint);

            if (signature == ori_signature)
                return true;
            else
                return false;
        }
        public string SignRequest(IDictionary<string, string> parameters, string endpoint)
        {
            // first : sort all key with asci order
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);
            
            // second : contact all params with key order
            StringBuilder query = new StringBuilder();
            query.Append(endpoint);
            foreach (KeyValuePair<string, string> kv in sortedParams)
            {
                if (!string.IsNullOrEmpty(kv.Key))// && !string.IsNullOrEmpty(kv.Value))
                {
                    if (kv.Value == "false")
                    {
                        query.Append(kv.Key).Append("False");
                    }
                    else if (kv.Value == "true")
                    {
                        query.Append(kv.Key).Append("True");
                    }
                    else
                    {
                        query.Append(kv.Key).Append(kv.Value);
                    }
                    
                }
            }
            //Console.WriteLine("data for making signanuture: {0}", query.ToString());

            // next : sign the string
            byte[] bytes = null;

                HMACSHA256 sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(this.token));
                bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));

            // finally : transfer binary byte to hex string
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}
