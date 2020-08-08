using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace StochasticControl.CommandLineParser
{
    public class Options
    {
        [Option('t', "time", Required = false, Default = 1.0, HelpText = "Time horizont of simulation")]
        public double T { get; set; }

        [Option('m', "steps", Required = false, Default = 100, HelpText = "Number of steps in time")]
        public int NbSteps { get; set; }

        [Option('s', "sigma", Required = true, HelpText = "Sigma")]
        public double Sigma { get; set; }

        [Option('i', "initialvalue", Required = false, Default = 1.0, HelpText = "Initial value of process at time t=0")]
        public double S0 { get; set; }

        [Option('r', "riskfreerate", Required = false, Default = 0.02, HelpText = "Risk free rate of return")]
        public double R { get; set; }

        [Option('n', "numbersimu", Required = false, Default = 1000, HelpText = "Number of simulations")]
        public int NbSimus { get; set; }

        [Option('d', "numberdiscretization", Required = false, Default = 100, HelpText = "Number of steps in discretization")]
        public int NbDiscr { get; set; }

        [Option('u', "Qmin", Required = true, HelpText = "Q min")]
        public double QMin { get; set; }

        [Option('v', "Qmax", Required = true, HelpText = "Q max")]
        public double QMax { get; set; }

        [Option('w', "deltaQmin", Required = true, HelpText = "q min")]
        public double DeltaQMin { get; set; }

        [Option('x', "DeltaQmax", Required = true, HelpText = "q max")]
        public double DeltaQMax { get; set; }

        [Option('y', "StepsQ", Required = true, HelpText = "dQ")]
        public int NbStepsQ { get; set; }

        public QSpace CreateQSpace()
        {
            return new QSpace(QMin, QMax, DeltaQMin, DeltaQMax, NbStepsQ);
        }
    }
}
