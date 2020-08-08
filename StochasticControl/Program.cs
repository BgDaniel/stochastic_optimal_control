using CalculationEngine;
using Models;
using Plot;
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
            //var model = new BinomialTree(options.S0, options.NbTimes, options.T, options.Sigma, options.R, options.NbSimus);

            Func<double, double> deterministicPath = t => 2.0 - .5 * t + .25 * Math.Sin(4.0 * Math.PI * t); 

            var model = new DeterministicPath(deterministicPath, 100, 1.0);
            model.Simulate();

            var QSpace = options.CreateQSpace();

            var optimalController = new OptimalController(model, QSpace);
            optimalController.Control();

            (var valueProcess, var Q, var q, var S) = optimalController.RollOut(0, options.Q0);
            var plotter = new Plotter(valueProcess, Q, q, S, model.Times);
            plotter.Plot();
            
        }
    }
}
