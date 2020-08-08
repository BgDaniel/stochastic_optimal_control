using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;

namespace Plot
{
    public class Plotter
    {
        public double[] ValueProcess { private set; get; }
        public double[] Q { private set; get; }
        public double[] Dq { private set; get; }
        public double[] S { private set; get; }
        public double[] Times { private set; get; }
        public int NbTimes { private set; get; }

        public Plotter(double[] valueProcess, double[] _Q, double[] q, double[] _S, double[] times)
        {
            ValueProcess = valueProcess;
            Q = _Q;
            Dq = q;
            S = _S;
            Times = times;
            NbTimes = Times.Length;
        }

        public void Plot()
        {
            var plt = new PlotModel();
            plt.Title = "Optimal Control";

            plt.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time" });

            plt.Series.Add(ToLineSeries(ValueProcess));
            plt.Series.Add(ToLineSeries(Q));
            plt.Series.Add(ToLineSeries(Dq));
            plt.Series.Add(ToLineSeries(S));
        }

        private LineSeries ToLineSeries(double[] values)
        {
            var lineSeries = new LineSeries();

            for (int i = 0; i < NbTimes; i++)
                lineSeries.Points.Add(new DataPoint(Times[i], values[i]));

            return lineSeries;
        }
    }
}
