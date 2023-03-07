using System;
using Entitys.Class;
using MiddelWareApp;
using Newtonsoft.Json;

namespace e_Invoice7._3._5
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                // here in this url we put the API location
                string TestUrl = "https://reqres.in/api/users";
                var _HttpOption = new HttpClientOperation<Users>();

                var res = _HttpOption.GetAsyncResult(TestUrl).Result;
                var JsonFormat = JsonConvert.SerializeObject(res);
                Console.WriteLine("The JsonFile Data Body : ");
                Console.WriteLine(JsonFormat.ToString());

                // Now we post the Data we get it in the previous function
                Users submissionResu = new Users();
                var postResult = _HttpOption.PostAsyncRequwst<Users>(TestUrl, res);
                Console.WriteLine("The   Data posted : ");
                Console.WriteLine(postResult.Result.support.url.ToString());

                // here we use the th urle that give us the Token
                var _Tget = new TokenGenerate<Token>();
                var token = _Tget.GenerateToken<Token>("https://id.preprod.eta.gov.eg" + "/connect/token");
                Console.WriteLine("The   Token Type Is   : ");
                Console.WriteLine(token.Result.token_type);
                Console.WriteLine("The   Token Key Is   : ");
                Console.WriteLine(token.Result.access_token);
                Console.WriteLine("The   Token WillExpire In     : ");
                Console.WriteLine(token.Result.expires_in);
            }
            catch (Exception ex)
            {
                Console.WriteLine("    Error     : " + ex.ToString());
            }
        }
    }
}
