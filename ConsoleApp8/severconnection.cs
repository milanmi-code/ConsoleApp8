using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;

namespace ConsoleApp8
{
    public class severconnection
    {
        HttpClient client = new HttpClient();
        string serverurl = "";
        public severconnection(string severurl) {
            this.serverurl = serverurl;
        }
        List<kolbasz> kolbaszlist = new List<kolbasz>();


        public async Task<List<kolbasz>>allkolbasz() {
            List<kolbasz> allkolbaszok = new List<kolbasz>();
            string url = serverurl + "/kolbaszok";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                allkolbaszok = JsonConvert.DeserializeObject<List<kolbasz>>(result).ToList();
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return allkolbaszok;
        }

        public async Task<bool> createkolbasz(string name, float rew, int price) {
            string url = serverurl + "/createkolobasz";
            try
            {
                var jsoninfo = new { kolbaszname = name, kolbaszertekeles = rew, kolbaszara = price };
                string jsonstringifed = JsonConvert.SerializeObject(jsoninfo);
                HttpContent sendthis = new StringContent(jsonstringifed, Encoding.UTF8, "Application/Json");
                HttpResponseMessage response = await client.PostAsync(url, sendthis);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                kolbasz data = JsonConvert.DeserializeObject<kolbasz>(result);
                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return false;
        }
    }
}
