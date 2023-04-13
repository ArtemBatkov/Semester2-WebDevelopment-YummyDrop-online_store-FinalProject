using Microsoft.AspNetCore.Components;

namespace YummyDrop_online_store.Services.ClientService
{
    public class ClientService : IClientService
    {
        
        public async Task<HttpResponseMessage> FetchDataFromAPI(string url)
        {
            try
            {
                var client = new HttpClient();
                HttpResponseMessage message = await client.GetAsync(url);
                return message;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        
    }
}
