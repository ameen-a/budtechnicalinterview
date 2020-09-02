using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using Newtonsoft.Json;


namespace BudTechInterview
{
    public class Country
    {
        
        string name { get; set; }
        string region { get; set; }
        string capitalCity { get; set; }
        float longitude { get; set; }
        float latitude { get; set; }


        static void Main(string[] args)
        {
            // create syntactically valid API request
            string ValidIso = GetValidIso();
            string ApiRequest = String.Format("http://api.worldbank.org/v2/country/{0}?format=json", ValidIso);

            // send request
            WebRequest request = WebRequest.Create(ApiRequest);
            WebResponse response = request.GetResponse();

            //read, then deserialise display JSON content
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                // 
                Country Country = JsonConvert.DeserializeObject<Country>(responseFromServer);

            }

        }

        static string GetValidIso()
        {
            Console.WriteLine("Enter a valid ISO code (2 or 3 letters)");
            
            while (true)
            {
                string iso = Console.ReadLine();
                // check if ISO follows double or triple letter regular expression
                if (Regex.IsMatch(iso, @"[A-Za-z]{2,3}"))
                {
                    return iso;
                }
                else
                {
                    Console.WriteLine("Invalid ISO - Please enter a valid 2 or 3 letter value");
                }
            }
        }
    }
}

