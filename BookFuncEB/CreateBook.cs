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
    public static class CreateBook
    {
        [FunctionName("CreateBook")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string title = req.Query["title"];
            string author = req.Query["author"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            title = title ?? data?.title;
            author = author ?? data?.author;

            string responseMessage = "hmmm... add title and author to the body in order to create a new book";

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(author))
            {
                responseMessage = $"{title} is written by {author}";
            }
            return new OkObjectResult(responseMessage);
        }
    }
}
