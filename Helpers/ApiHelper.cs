using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Helpers
{
    public class ApiHelper
    {
        private const string DefaultAPIPath= "http://localhost:5292";
        private const string AndroidAPIPath = "http://10.0.2.2:5292";


        public ApiHelper() { }


        public HttpClient GetHttpClient() 
        {
            HttpClient httpClient = new HttpClient();

#if ANDROID
            httpClient.BaseAddress = new Uri(AndroidAPIPath);

#else
            httpClient.BaseAddress = new Uri(DefaultAPIPath);
#endif

            return httpClient;
        }
       
    }
}
