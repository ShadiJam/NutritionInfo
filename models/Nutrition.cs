using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

internal class Fields
{
    public string item_id { get; set; }
    public string item_name { get; set; }
    public string brand_name { get; set; }
    public double nf_calories { get; set; }
    public double nf_total_fat { get; set; }
    public int nf_serving_size_qty { get; set; }
    public string nf_serving_size_unit { get; set; }
}

internal class Hit
{
    public string _index { get; set; }
    public string _type { get; set; }
    public string _id { get; set; }
    public double _score { get; set; }
    public Fields fields { get; set; }
}

internal class Nutrition
{
    public int total_hits { get; set; }
    public double max_score { get; set; }
    public List<Hit> hits { get; set; }
}