using CalculationEngine;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StochasticControl
{
    public class FileWriter
    {
        public string PathToFolder;
        public string FileName;

        public FileWriter(string pathToFolder, string fileName)
        {
            PathToFolder = pathToFolder;
            FileName = fileName;
        }

        public void WriteToFile(QStep[] qSteps)
        {
            var engine = new FileHelperEngine<QStep>
            {
                HeaderText = typeof(QStep).GetCsvHeader();
            };

            var stringBuilder = new StringBuilder(PathToFolder);
            stringBuilder.Append(FileName);
            var fullPath = stringBuilder.ToString();
            
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            engine.HeaderText = engine.GetFileHeader();
            engine.AppendToFile(fullPath, qSteps);
        }
    }
}
