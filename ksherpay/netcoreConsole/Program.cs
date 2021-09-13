using System;
using System.Collections.Generic;
using Ksherpay;
namespace netcoreConsole
{
    class Program
    {

        static string base_url = @"https://sandboxdoc.vip.ksher.net";
        static string token = "your token";
        
        static void Main(string[] args)
        {

            Ksherpay.Ksherpay ksherpay_redirect = new Ksherpay.Ksherpay(base_url, ApiType.redirect, token);
            Ksherpay.Ksherpay ksherpay_cscanb = new Ksherpay.Ksherpay(base_url, ApiType.cscanb, token);
            string cmd = "";

            while (cmd != "9")
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
                    string merchant_order_id=Ksherpay.MyUtil.GenerateTimestamp();

                    Console.WriteLine("Enter amount (int only, Enter 150 is 1.50): ");
                    string amount = Console.ReadLine();

                    createRequest.Add("merchant_order_id", merchant_order_id);
                    createRequest.Add("amount", amount);
                    createRequest.Add("redirect_url", @"https://webhook.site/effdbb5f-0c80-4efe-b7e8-c9f9585461d8/pass");
                    createRequest.Add("redirect_url_fail", @"https://webhook.site/effdbb5f-0c80-4efe-b7e8-c9f9585461d8/fail");
                    //createRequest.Add("channel", "ktbcard"); if not add will display all of wallet
                    createRequest.Add("note", "test order");

                    var response_create = ksherpay_redirect.create(createRequest);
                    logDictionary(response_create);
                }

                else if (cmd == "2")
                {
                    Console.WriteLine("Enter merchant_order_id: ");
                    string merchant_order_id = Console.ReadLine();

                    IDictionary<string, string> queryRequest = new Dictionary<string, string>();
                    queryRequest.Add("order_id", merchant_order_id);

                    var response_query = ksherpay_redirect.query(queryRequest);
                    logDictionary(response_query);
                }
                else if (cmd == "3")
                {
                    Console.WriteLine("Enter merchant_order_id: ");
                    string merchant_order_id = Console.ReadLine();

                    Console.WriteLine("Enter amount (int only, Enter 150 is 1.50): ");
                    string amount = Console.ReadLine();

                    string refund_order_id = Ksherpay.MyUtil.GenerateTimestamp();

                    IDictionary<string, string> refundRequest = new Dictionary<string, string>();
                    refundRequest.Add("order_id", merchant_order_id);
                    refundRequest.Add("refund_order_id", refund_order_id);
                    refundRequest.Add("refund_amount", amount);

                    var response_query = ksherpay_redirect.refund(refundRequest);
                    logDictionary(response_query);
                }
                else if (cmd == "5")
                {
                    string merchant_order_id = Ksherpay.MyUtil.GenerateTimestamp();

                    Console.WriteLine("Enter amount (int only, Enter 150 is 1.50): ");
                    string amount = Console.ReadLine();

                    Console.WriteLine("Enter channel (alipay,wechat,airpay,promptpay,truemoney) ");
                    Console.WriteLine("(Please check mid type support for make sure account support)");
                    string channel = Console.ReadLine();


                    IDictionary<string, string> createRequest = new Dictionary<string, string>();
                    createRequest.Add("merchant_order_id", merchant_order_id);
                    createRequest.Add("amount", amount);
                    createRequest.Add("channel", channel);
                    createRequest.Add("note", "test order");

                    var response_create = ksherpay_cscanb.create(createRequest);
                    logDictionary(response_create);
                }

                else if (cmd == "6")
                {
                    Console.WriteLine("Enter merchant_order_id: ");
                    string merchant_order_id = Console.ReadLine();

                    IDictionary<string, string> queryRequest = new Dictionary<string, string>();
                    queryRequest.Add("order_id", merchant_order_id);

                    var response_query = ksherpay_redirect.query(queryRequest);
                    logDictionary(response_query);
                }
                else if (cmd == "7")
                {
                    Console.WriteLine("Enter merchant_order_id: ");
                    string merchant_order_id = Console.ReadLine();

                    Console.WriteLine("Enter amount (int only, Enter 150 is 1.50): ");
                    string amount = Console.ReadLine();

                    string refund_order_id = Ksherpay.MyUtil.GenerateTimestamp();

                    IDictionary<string, string> refundRequest = new Dictionary<string, string>();
                    refundRequest.Add("order_id", merchant_order_id);
                    refundRequest.Add("refund_order_id", refund_order_id);
                    refundRequest.Add("refund_amount", amount);

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
