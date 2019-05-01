using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using tgsc_functions.Models;
using System.Linq;

namespace tgsc_functions
{
    public static class PlayerScoringAverage
    {
        [FunctionName("PlayerScoringAverage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("The PlayerScoringAverage method was called.");

            // Retreive the player id from the querystring.
            string playerId = req.Query["playerid"];

            // Call the tgsc API to return all rounds for this player.
            var client = new RestClient("https://tgsc-api.azurewebsites.net/api");
            var request = new RestRequest("players/" + playerId + "/rounds", DataFormat.Json);
            var rounds = client.Get<List<Round>>(request).Data;

            // Determine the scoring average.
            double scoringAvg = Math.Round(rounds.Average(r => r.Score), 1);

            return playerId != null
                ? (ActionResult)new OkObjectResult($"Hello, {playerId}, your scoring average is: " + scoringAvg.ToString())
                : new BadRequestObjectResult("Please pass a player id on the query string or in the request body");
        }
    }
}
