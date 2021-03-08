using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class HelloPublicService
    {
        public async Task<string> HelloPublic()
        {
            using (var http = new HttpClient())
            {
                var response = await http.GetAsync("https://r4kvcid191.execute-api.ap-southeast-1.amazonaws.com/oh-lambda-dotnet-public/hellopublic");
                if (!response.IsSuccessStatusCode)
                {
                    return "Request to public server failed.";
                }
                else
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
