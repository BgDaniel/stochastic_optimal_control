using CalculationEngine;
using FileHelpers;
using Models;
using Plot;
using StochasticControl.CommandLineParser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Text;

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
            //var model = new BinomialTree(options.S0, options.NbTimes, options.T, options.Sigma, options.R, options.NbSimus);

            Func<double, double> deterministicPath = t => 2.0 - .5 * t + .25 * Math.Sin(4.0 * Math.PI * t); 

            var model = new DeterministicPath(deterministicPath, 100, 1.0);
            model.Simulate();

            var QSpace = options.CreateQSpace();

            var optimalController = new OptimalController(model, QSpace);
            optimalController.Control();

            var qSteps = optimalController.RollOut(0, options.Q0);

            var fileWrite = new FileWriter("C:\\Users\\bergerd\\", "q_Steps.csv");
            fileWrite.WriteToFile(qSteps);
        }
    }
}
