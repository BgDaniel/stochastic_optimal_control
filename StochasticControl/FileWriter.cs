using CalculationEngine;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace StochasticControl
{
    public class FileWriter
    {
        public string FileName;

        public FileWriter(string fileName)
        {
            FileName = fileName;
        }

        public void WriteToFile(QStep[] qSteps)
        {
            var engine = new FileHelperEngine<QStep>();

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            var fullPath = Path.Combine(new string[] { projectDirectory, "Data", FileName });
            
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            engine.HeaderText = engine.GetFileHeader();
            engine.AppendToFile(fullPath, qSteps);
        }
    }
}
