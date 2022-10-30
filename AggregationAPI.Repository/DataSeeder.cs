using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using AggregationAPI.Models.Data;
using AggregationAPI.Models;

namespace AggregationAPI.Extensions
{
    public static class DataSeeder
    {
        private static readonly HttpClient client = new HttpClient();
        public async static Task<List<Record>> SeedData(DataSource dataSource)
        {

            var data = new List<Record>();

            foreach (var param in dataSource.Parameters)
            {
                var responseString = await client.GetStringAsync(dataSource.Url + param);
                var resp = JsonConvert.DeserializeObject<List<List<string>>>(responseString);
                resp.RemoveAt(0);
                var parsedData = resp.Select(x => new Record
                {
                    TINKLAS = x[0],
                    OBT_PAVADINIMAS = x[1],
                    OBJ_GV_TIPAS = x[2],
                    OBJ_NUMERIS = int.Parse(x[3]),
                    P_PLUS = Convert.ToDouble(x[4]),
                    PL_T = DateTime.Parse(x[5]),
                    P_MINUS = Convert.ToDouble(x[6])
                }).ToList();


                data.AddRange(parsedData);
            }

            return data;

        }
    }
}