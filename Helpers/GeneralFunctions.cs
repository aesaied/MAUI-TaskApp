using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Helpers
{
    public static class GeneralFunctions
    {

        public static async Task ViewErrors(this HttpResponseMessage result)
        {
            var responseText = await result.Content.ReadAsStringAsync();


            if (responseText.StartsWith("{\"\":"))
            {
                responseText = responseText.Replace("{\"\":", "").Replace("}", "");

                var errors = JsonConvert.DeserializeObject<List<string>>(responseText);

                await AppShell.Current.DisplayAlert("Error", string.Join("\n", errors), "Ok");
            }
        }
    }
}
