using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class HelloPrivateService
    {
        public async Task<string> HelloPrivate()
        {
            using (var http = new HttpClient())
            {
                var response = http.GetAsync("http://10.3.1.64/webapplication/hello"); // private
                //var response = http.GetAsync("http://18.141.230.53/webapplication/hello"); // public
                if (!response.Result.IsSuccessStatusCode)
                {
                    return "Request to private server failed.";
                }
                else
                {
                    return await response.Result.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
