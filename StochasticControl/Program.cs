using CalculationEngine;
using Models;
using StochasticControl.CommandLineParser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StochasticControl
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute(ExampleConfigs.ExampleConfigurations.Geom1);
        }

        private static void Execute(Options options)
        {
            var model = new BinomialTree(options.S0, options.NbTimes, options.T, options.Sigma, options.R, options.NbSimus);

            Func<double, double> deterministicPath = t => 2.0 - .5 * t + .25 * Math.Sin(4.0 * Math.PI * t); 

            //var model = new DeterministicPath(deterministicPath, 200, 2.0);
            model.Simulate();

            var QSpace = options.CreateQSpace();

            var optimalController = new OptimalController(model, QSpace);
            optimalController.Control();

            (var valueProcess, var Q, var q, var S) = optimalController.RollOut(0, options.Q0);


            //rollOut.WriteToFile("Z:\\csharp\\stochastic_optimal_control\\optimal_control\\paths_rolled_out", new List<int>() { 0 });
        }
    }
}
