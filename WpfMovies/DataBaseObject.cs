using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfMovies
{
    internal class DataBaseObject
    {
        public async void addEntityInstance<T>(T i_EntityInstance , string i_URL)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync(i_URL);
            if(message.IsSuccessStatusCode)
            {
                var jsonString = message.Content.ReadAsStringAsync();
                Type entityType = i_EntityInstance.GetType();
            }
            
            
        }
    }
}
