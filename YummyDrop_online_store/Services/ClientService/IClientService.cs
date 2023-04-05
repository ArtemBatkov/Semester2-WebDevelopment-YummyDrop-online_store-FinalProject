using Microsoft.AspNetCore.Components;

namespace YummyDrop_online_store.Services.ClientService
{
    interface IClientService
    {
        public Task<HttpResponseMessage> FetchDataFromAPI(string url);
        public   int GetBrowserPort(NavigationManager navigationManager);
    }
}
