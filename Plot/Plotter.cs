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
            var model = new PlotModel();
            model.Title = "Optimal Control";

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time" });

            model.Series.Add(ToLineSeries(ValueProcess, "optimal control value", OxyColors.Red));
            model.Series.Add(ToLineSeries(Q, "total storag volume", OxyColors.Blue));
            model.Series.Add(ToLineSeries(Dq, "change in storage volume", OxyColors.Green));
            model.Series.Add(ToLineSeries(S, "underlying price", OxyColors.Orange));
        }

        private LineSeries ToLineSeries(double[] values, string title, OxyColor color)
        {
            var lineSeries = new LineSeries() { 
                Title = title,
                Color = color
            };

            for (int i = 0; i < NbTimes; i++)
                lineSeries.Points.Add(new DataPoint(Times[i], values[i]));

            return lineSeries;
        }
    }
}
