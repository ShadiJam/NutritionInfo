using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;


// all data structures are IEnumerables and can use applicable extension methods.
// interfaces will always expect a return type
// implicitly non-static and public

//internal is the standard for interfaces unless you want others to be able to use it
internal interface IJSONAPI {
    Task<string> GetJSON(string term, string key);
    Task<T> GetData<T>(string term, string key);
    string ToJSON(Object o);
}

// MashapeAPI class inherits from IJSONAPI
internal class MashapeAPI : IJSONAPI {
    // list of fields to be used with Mashape API
    private List<string> fields = new List<string>() {
        "item_name",
        "item_id",
        "brand_name",
        "nf_calories",
        "nf_total_fat"
    };
    // string to be used in the HTTP request below
    public string urlFormat(string term) =>
        $"https://nutritionix-api.p.mashape.com/v1_1/search/{term}?fields={String.Join(",", fields)}";

    // adds key into the header in mashape API
    // Task that receives JSON from a request from the Mashape API
    public async Task<string> GetJSON(string term, string key){
        // initialize new HTTP client
        var http = new HttpClient();
        // setup up the request message and add associated header for the
        // Mashape key
        var request = new HttpRequestMessage(HttpMethod.Get, urlFormat(term));
        request.Headers.Add("-X-Mashape-Key", key);
        // send the request and assign to variable "reply
        var reply = await http.SendAsync(request);
        // read content as a string and store as "result"
        var result = await reply.Content.ReadAsStringAsync();
        return result;
    }
    // Task returns that data within the JSON
    public async Task<T> GetData<T>(string term, string key){
        // uses the GetJSON method to get a JSON string called "json"
        string json = await GetJSON(term, key);
        // Deserializes "json" sand stores as Type T "instance"
        T instance = JsonConvert.DeserializeObject<T>(json);
        // returns the instance to be used within the assembly
        return instance;
    }
    // Serializes Object o to JSON
    public string ToJSON(Object o){
        return JsonConvert.SerializeObject(o);
    }

}

// GoogleAPI class that inherits from interface IJSONAPI
internal class GoogleAPI : IJSONAPI {
    // string to be used in the HTTP request below
    public string urlFormat(string term, string key) =>
        $"https://maps.googleapis.com/maps/api/geocode/json?address={term}&key={key}";
    // Task that receives JSON from a request to the Google GeoCoding API
    public async Task<string> GetJSON(string term, string key){
        var http = new HttpClient();
        var result = await http.GetStringAsync(urlFormat(term, key));
        return result;
    }
    // Task returns that data within the JSON
    public async Task<T> GetData<T>(string term, string key){
        string json = await GetJSON(term, key);
        T instance = JsonConvert.DeserializeObject<T>(json);
        return instance;
    }
    // method that serializes Object o into JSON
    public string ToJSON(Object o){
        return JsonConvert.SerializeObject(o);
    }
}
