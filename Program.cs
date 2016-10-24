using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

public class Program
{
    public static void Main(string[] args)
    {
        prompt().Wait();
    }

    public static async Task prompt()
    {
        Console.WriteLine(@"
        Hey! Listen!
        
        --------
        
        Enter a food or beverage to get caloric info:


        ");
        
        string term = Console.ReadLine();
        string key = "49rhltbtvSmshKivQd2EvXliQ0Zrp1vCsZ5jsnVtER81HAflRg";
        IJSONAPI mashUrl = new MashapeAPI();
        
        
        Nutrition data = await mashUrl.GetData<Nutrition>(term, key);
        string result = data.nutrition.ToString();
        Console.WriteLine();
        // Nutrition n = await mashapi.GetData<Nutrition>("cheddar cheese", "APIKEY");
        // Console.WriteLine(mashapi.ToJSON(n));  Gaucho: get JSON should work across
        //all child classes

        // IJSONAPI googapi = new GoogleAPI();   ea. API will either have a header info(use) Postman
                                                                        //if header required.
        // Google g = await googapi.GetData<Google>("tacos", "APIKEY"); //google: no header, api key appended to end.
        // Console.WriteLine(googapi.ToJSON(g));
    }
}
