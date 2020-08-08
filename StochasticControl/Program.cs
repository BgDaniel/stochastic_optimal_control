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
            var gbm = new GeometricBrownianMotion(options.T, options.NbSteps, options.Sigma, options.S0, options.R, options.NbSimus);
            var paths = gbm.Simulate();

            var optimalController = new OptimalController(paths, options.NbDiscr, gbm, options.QMin, options.QMax, options.Qmin,
                options.Qmax, options.StepsQ);
            var pathIndices = optimalController.PathIndices;
            var J = optimalController.Control();

            var rollOut = new RollOut(J, paths, pathIndices, gbm, 10, optimalController);

            rollOut.WriteToFile("Z:\\csharp\\stochastic_optimal_control\\optimal_control\\paths_rolled_out", new List<int>() { 0 });
        }
    }
}
