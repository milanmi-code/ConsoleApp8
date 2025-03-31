﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;

namespace ConsoleApp7
{
    public class ServerConnection
    {
        public HttpClient client = new HttpClient();
        public string serverUrl = "";
        public ServerConnection(string serverUrl)
        {
            this.serverUrl = serverUrl;
        }

        public async Task<List<kolbasz>> AllKolbi()
        {
            List<kolbasz> allkolbi = new List<kolbasz>();

            string url = serverUrl + "/kolbaszok";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                allkolbi = JsonConvert.DeserializeObject<List<kolbasz>>(result).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return allkolbi;
        }
        public async Task<bool> createKolbi(string name, float ertekeles, int ar)
        {
            string url = serverUrl + "/createKolbasz";
            try
            {
                var jsonInfo = new
                {
                    kolbaszNeve = name,
                    kolbaszErtekelese = ertekeles,
                    kolbaszAra = ar
                };
                string jsonstring = JsonConvert.SerializeObject(jsonInfo);
                HttpContent sendthis = new StringContent(jsonstring, Encoding.UTF8, "Application/json");
                HttpResponseMessage response = await client.PostAsync(url, sendthis);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                kolbasz data = JsonConvert.DeserializeObject<kolbasz>(result);
                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            return false;
        }
        public async Task<bool> deleteKolbi(int id)
        {
            string url = serverUrl + "/deleteKolbasz/" + id;
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            return false;
        }
    }
}