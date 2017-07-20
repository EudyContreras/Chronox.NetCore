using Chronox.Utilities.Extenssions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Chronox.Tests.Utilitities
{
    internal class TestMethodGenerator
    {
        public static void Generate(string[] expressions, DateTime[] ExpectedResults, string path)
        {
            var lines = File.ReadAllLines(path);

            var newLines = new List<string>();

            for (int i = 0; i < expressions.Length; i++)
            {
                foreach (var line in lines)
                {
                    var newLine = line;

                    newLine = newLine.Replace("&", ExpectedResults[i].ToString(), true);
                    newLine = newLine.Replace("#", expressions[i], true);
                    newLine = newLine.Replace("?", i.ToString(), true);

                    newLines.Add(newLine);
                }
            }

            File.AppendAllLines(path, newLines);
        }
    }
}
