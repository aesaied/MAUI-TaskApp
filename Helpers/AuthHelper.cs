using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Model;


using System.Text.Json.Serialization;

namespace TaskApp.Helpers
{
    public class AuthHelper
    {
        private readonly ApiHelper _apiHelper;
        public AuthHelper(ApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }


        public async Task<bool> Login(string username, string password)
        {
            var cansToken = new CancellationToken();

            var httpClient = _apiHelper.GetHttpClient();
            var result = await httpClient.PostAsJsonAsync("/api/account/login", new LoginInput() { UserName = username, Password = password }, cansToken);


            if (result != null && result.IsSuccessStatusCode)
            {
                var responseText = await result.Content.ReadAsStringAsync();

                var loginResult = JsonConvert.DeserializeObject<LoginResultDto>(responseText);

                await SecureStorage.Default.SetAsync("AuthToken", loginResult?.Token!);

             
                return true;
            }

            else
            {
                var responseText = await result.Content.ReadAsStringAsync();


                if (responseText.StartsWith("{\"\":"))
                {
                    responseText = responseText.Replace("{\"\":", "").Replace("}", "");

                    var errors = JsonConvert.DeserializeObject<List<string>>(responseText);

                 await AppShell.Current.DisplayAlert("Error", string.Join("\n", errors), "Ok");
                }
            }

            return false;
        }
    }
}
