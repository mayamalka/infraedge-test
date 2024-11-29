using RestSharp;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Text.RegularExpressions;
using OpenQA.Selenium.BiDi.Modules.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;

namespace infraedgeTest
{
    public class wikiTestApi
    {
        [Test]
        public void ApiTest()
        {
            RestClient client = new RestClient("https://en.wikipedia.org/w/api.php");
            RestRequest request = new RestRequest();

            request.AddParameter("action", "query");
            request.AddParameter("format", "json");
            request.AddParameter("titles", "Test_automation");
            request.AddParameter("prop", "revisions");
            request.AddParameter("rvprop", "content");
            request.AddParameter("rvsection", "8"); //uncoverd through sending the request on postman to figure out section number

            RestResponse response = client.Get(request);

            JObject jsonResponse = JObject.Parse(response.Content);

            string extract = jsonResponse["query"]["pages"]["1086547"]["revisions"][0]["*"].ToString();

            Dictionary<string, int> UniqueWords = utilityFunctions.GetUniqueWords(extract);

            foreach (KeyValuePair<string, int> UniqueWord in UniqueWords)
            {
                Console.WriteLine($"{UniqueWord.Key}: {UniqueWord.Value}");
            }
        }
    }
}
