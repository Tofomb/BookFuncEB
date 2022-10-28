using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookFuncEB
{
    public static class Plunction
    {
        [FunctionName("Plunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            string lame = req.Query["lame"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            lame = lame ?? data?.lame;

            string responseMessage = "hmmm...";

            if (!string.IsNullOrEmpty(lame) && !string.IsNullOrEmpty(name))
            {
                responseMessage = $"{lame} {name} or something";
            }
            return new OkObjectResult(responseMessage);
        }
    }
}
