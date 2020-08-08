using CalculationEngine;
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
            var model = new BinomialTree(options.S0, options.NbTimes, options.T, options.Sigma, options.R);

            var QSpace = options.CreateQSpace();

            var optimalController = new OptimalController(model, QSpace);
            
            var optimalValues = optimalController.Control();



            //rollOut.WriteToFile("Z:\\csharp\\stochastic_optimal_control\\optimal_control\\paths_rolled_out", new List<int>() { 0 });
        }
    }
}
