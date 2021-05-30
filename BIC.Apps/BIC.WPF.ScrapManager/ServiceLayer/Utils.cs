using BIC.WPF.ScrapManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.ServiceLayer
{
    public static class Utils
    {
        public static void ReadCommandYaml(this CommandCache commandCache, string fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);

            string     currentCommandLine;
            CommandExe currentCommand = null;
            for (int i = 0; i < lines.Count(); i++)
            {
                var depth = GetDepth(lines[i]);
                if (depth == 0) // first line must be command anyway
                {
                    currentCommandLine = lines[i];
                    currentCommand = new CommandExe() { CommandLine = currentCommandLine };
                    commandCache.CommandLines.Add(currentCommand);
                }
                else
                {
                    currentCommand.CommandParameters = ReadParameters(lines, ref i, depth);
                }
            }
        }

        private static List<CommandParameter> ReadParameters(string[] lines, ref int currentI, int currentDepth)
        {
            List<CommandParameter> newCommandParameters = new List<CommandParameter>();

            for (int i = currentI; i < lines.Count(); i++)
            {
                var depth = GetDepth(lines[i]);
                if (depth == currentDepth)
                {
                    var p = new CommandParameter() { ParameterLine = lines[i] };
                    currentI = i;
                    newCommandParameters.Add(p);
                }
                else if (depth == currentDepth + 1)
                {
                    newCommandParameters.Last().DrillDownParameters = ReadParameters(lines, ref i, depth);
                }
                else if (depth == currentDepth - 1)
                {
                    currentI = i;
                    return newCommandParameters;
                }
            }

            return newCommandParameters;
        }
        private static int GetDepth(string line)
        {
            Regex rg = new Regex("^ *");
            if (!rg.IsMatch(line))
                return 0;
            else
            {
                var line1 = rg.Match(line).Value;
                return line1.Count(s => s == ' ')/2;
            }
        }
    }
}
