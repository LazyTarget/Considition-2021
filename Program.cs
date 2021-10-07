
using System;
using System.Linq;
using System.Text.Json;
using System.Xml;
using DotNet.Responses;

namespace DotNet
{
    public static class Program
    {
        // The different map names can be found on considition.com/rules
        
        public static void Main(string[] args)
        {
            var apiKey = args[0];
            var map = args.ElementAtOrDefault(1) ?? "training1";
            var GameLayer = new GameLayer(apiKey);

            var gameInformation = GameLayer.NewGame(map);
            var solver = new RandomSolver(gameInformation.Dimensions, gameInformation.Vehicle);
            var solution = solver.Solve();
            var submitSolution = GameLayer.Submit(JsonSerializer.Serialize(solution), map);
           
            Console.WriteLine("Your GameId is: " + submitSolution.GameId);
            Console.WriteLine("Your score is: " + submitSolution.Score);
            Console.WriteLine("Link to visualisation" + submitSolution.Link);
            Console.WriteLine("..for map: " + map);

        }
    }
}
