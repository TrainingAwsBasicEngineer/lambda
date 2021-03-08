using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route("UploadToS3")]
        public async Task<string> UploadToS3()
        {
            using (var client = new AmazonS3Client("AKIA4AYH52MIBEPN45WT", "JnC8Xxe1b1cyFd4QV5hVT7XvJVLYnnk5PDky76Y4+VdzpIUi6yqHFN", RegionEndpoint.APSoutheast1))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        FilePath = "test.png",
                        Key = "oh-UploadToS3.png",
                        BucketName = "oh-77110",
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }

                return "OK";
            }
        }


        [HttpGet]
        [Route("PresignedURL")]
        public async Task<string> PresignedURL()
        {
            using (var client = new AmazonS3Client("AKIA4AYH52MIBEPN45WT", "JnC8Xxe1b1cyFd4QV5hVT7XvJVLYnnk5PDky76Y4+VdzpIUi6yqHFN", RegionEndpoint.APSoutheast1))
            {
                GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                {
                    BucketName = "test-coded789-bankith",
                    Key = "test.png",
                    Expires = DateTime.UtcNow.AddMinutes(10)
                };
                var urlString = client.GetPreSignedURL(request1);

                return urlString;
            }

        }




        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {


            this._logger.LogInformation("Test Logging");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
