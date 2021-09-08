using System;
using System.Collections.Generic;

namespace ksherpay
{
    class Program
    {

        static string base_url = @"https://sandboxdoc.vip.ksher.net";
        static string token = "your token";
        static void Main(string[] args)
        {
            Ksherpay ksherpay_redirect = new Ksherpay(base_url, ApiType.redirect, token);
            Ksherpay ksherpay_cscanb = new Ksherpay(base_url, ApiType.cscanb, token);
            string cmd = "";

            while (cmd!="9")
            {
                Console.WriteLine("--- redirect API ---");
                Console.WriteLine("1 - create order");
                Console.WriteLine("2 - query order");
                Console.WriteLine("3 - refund order");
                Console.WriteLine("4 - delete order - not support at now");
                Console.WriteLine("--- cscanb API ---");
                Console.WriteLine("5 - create order");
                Console.WriteLine("6 - query order");
                Console.WriteLine("7 - refund order");
                Console.WriteLine("8 - delete order - not support at now");
                Console.WriteLine("--- other ---");
                Console.WriteLine("9 - exit");

                cmd = Console.ReadLine();


                if (cmd == "1")
                {
                    IDictionary<string, string> createRequest = new Dictionary<string, string>();
                    createRequest.Add("merchant_order_id", "hello");
                    createRequest.Add("amount", "100");
                    createRequest.Add("redirect_url", @"https://webhook.site/effdbb5f-0c80-4efe-b7e8-c9f9585461d8/pass");
                    createRequest.Add("redirect_url_fail", @"https://webhook.site/effdbb5f-0c80-4efe-b7e8-c9f9585461d8/fail");
                    createRequest.Add("note", "test order");

                    var response_create = ksherpay_redirect.create(createRequest);
                    logDictionary(response_create);
                }

                else if (cmd == "2")
                {
                    IDictionary<string, string> queryRequest = new Dictionary<string, string>();
                    queryRequest.Add("order_id", "hello");

                    var response_query = ksherpay_redirect.query(queryRequest);
                    logDictionary(response_query);
                }
                else if (cmd == "3")
                {
                    IDictionary<string, string> refundRequest = new Dictionary<string, string>();
                    refundRequest.Add("order_id", "hello");
                    refundRequest.Add("refund_order_id", "refund_hello");
                    refundRequest.Add("refund_amount", "100");

                    var response_query = ksherpay_redirect.refund(refundRequest);
                    logDictionary(response_query);
                }
                else if(cmd == "5")
                {
                    IDictionary<string, string> createRequest = new Dictionary<string, string>();
                    createRequest.Add("merchant_order_id", "test_hello2");
                    createRequest.Add("amount", "100");
                    createRequest.Add("channel", "truemoney");
                    createRequest.Add("note", "test order");

                    var response_create = ksherpay_cscanb.create(createRequest);
                    logDictionary(response_create);
                }

                else if (cmd == "6")
                {
                    IDictionary<string, string> queryRequest = new Dictionary<string, string>();
                    queryRequest.Add("order_id", "test_hello2");

                    var response_query = ksherpay_redirect.query(queryRequest);
                    logDictionary(response_query);
                }
                else if (cmd == "7")
                {
                    IDictionary<string, string> refundRequest = new Dictionary<string, string>();
                    refundRequest.Add("order_id", "test_hello2");
                    refundRequest.Add("refund_order_id", "refund_test_hello2");
                    refundRequest.Add("refund_amount", "100");

                    var response_refund = ksherpay_redirect.refund(refundRequest);
                    logDictionary(response_refund);
                }
                Console.WriteLine("===========");
            }
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
    }
}
