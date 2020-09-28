using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ModelLib.Model;
using Newtonsoft.Json;

namespace ConsumeRest
{
    internal class RestWorker
    {
        private const String URI = "http://localhost:58616/api/Items";

        public RestWorker()
        {

        }

        public async void Start()
        {
            IList<Item> allItems = await GetAllItemsAsync();
            foreach (Item item in allItems)
            {
                Console.WriteLine(item);
            }

            try
            {
                Item item1 = await GetOneItemsAsync(3);
                Console.WriteLine("item= " + item1);
            }
            catch (KeyNotFoundException knfe)
            {
                Console.WriteLine(knfe.Message);
            }
            
        }



        public async Task<IList<Item>> GetAllItemsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync(URI);
                IList<Item> items =
                    JsonConvert.DeserializeObject<IList<Item>>(json);
                return items;
            }
        }

        private async Task<Item> GetOneItemsAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var resp = await client.GetAsync(URI + id);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    Item item = JsonConvert.DeserializeObject<Item>(json);
                    return item;
                }

                throw new KeyNotFoundException($"Fejl code={resp.StatusCode} message={await resp.Content.ReadAsStringAsync()}");
            }
        }
    }
}