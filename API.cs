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

internal class MashapeAPI : IJSONAPI {

    private List<string> fields = new List<string>() {
        "item_name",
        "item_id",
        "brand_name",
        "nf_calories",
        "nf_total_fat"
    };

    public string urlFormat(string term) =>
        $"https://nutritionix-api.p.mashape.com/v1_1/search/{term}?fields={String.Join(",", fields)}";
    // adds key into the header in mashape API
    public async Task<string> GetJSON(string term, string key){
        var http = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, urlFormat(term));
        request.Headers.Add("-X-Mashape-Key", key);
        var reply = await http.SendAsync(request);
        var result = await reply.Content.ReadAsStringAsync();
        return result;
    }

    public async Task<T> GetData<T>(string term, string key){
        string json = await GetJSON(term, key);
        T instance = JsonConvert.DeserializeObject<T>(json);
        return instance;
    }

    public string ToJSON(Object o){
        return JsonConvert.SerializeObject(o);
    }

}
// you can override inheritance but not interfaces
internal class GoogleAPI : IJSONAPI {
    public string urlFormat(string term, string key) =>
        $"https://maps.googleapis.com/maps/api/geocode/json?address={term}&key={key}";

    public async Task<string> GetJSON(string term, string key){
        var http = new HttpClient();
        var result = await http.GetStringAsync(urlFormat(term, key));
        return result;
    }

    public async Task<T> GetData<T>(string term, string key){
        string json = await GetJSON(term, key);
        T instance = JsonConvert.DeserializeObject<T>(json);
        return instance;
    }

    public string ToJSON(Object o){
        return JsonConvert.SerializeObject(o);
    }
} 
