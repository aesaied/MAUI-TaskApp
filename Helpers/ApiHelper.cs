using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Helpers
{
    public class ApiHelper
    {
        private const string DefaultAPIPath= "http://localhost:5292";
        private const string AndroidAPIPath = "http://10.0.2.2:5292";


        public ApiHelper() { }


        public async Task<HttpClient> GetHttpClient() 
        {
            HttpClient httpClient = new HttpClient();
            //
            var token = await GetAuthToken();

            if (!string.IsNullOrWhiteSpace(token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"{token}");
            }

#if ANDROID
            httpClient.BaseAddress = new Uri(AndroidAPIPath);

#else
            httpClient.BaseAddress = new Uri(DefaultAPIPath);
#endif

            return httpClient;
        }



        public async Task SetAuthToken(string token)
        {
            await SecureStorage.Default.SetAsync("AuthToken", token!);
        }

        private async Task<string?> GetAuthToken()
        {
          return  await SecureStorage.Default.GetAsync("AuthToken");
        }

    }
}
