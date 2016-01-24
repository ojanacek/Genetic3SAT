using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Genetic3SAT
{
    public static class SatFormulaLoader
    {
        public static IEnumerable<BooleanFormula> LoadFormulas(string directoryPath, double clauseVariableRatio)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"Directory does not exist. {directoryPath}");

            return Directory.EnumerateFiles(directoryPath, "*.cnf").Select(f => LoadDimacsCnf(f, clauseVariableRatio));
        }

        public static BooleanFormula LoadDimacsCnf(string filePath, double clauseVariableRatio)
        {
            int[] weights = null, formula = null;
            int variableCount = 0, clauseCount = 0;

            int counter = 0;
            foreach (var line in File.ReadAllLines(filePath).Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var linePrefix = line[0];
                switch (linePrefix)
                {
                    case 'c':
                        break;
                    case 'w':
                    {
                        weights = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                      .Skip(1)
                                      .Select(int.Parse)
                                      .ToArray();
                        break;
                    }
                    case 'p':
                    {
                        var preambleValues = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                                 .Skip(2)
                                                 .Select(int.Parse)
                                                 .ToArray();
                        variableCount = preambleValues[0];
                        clauseCount = preambleValues[1];

                        if ((double) clauseCount / variableCount > clauseVariableRatio)
                            clauseCount = (int)(variableCount * clauseVariableRatio);

                        if (weights == null)
                            throw new InvalidDataException("No weights found.");

                        if (weights.Length != variableCount)
                            throw new InvalidDataException("There are extra or missing weights.");

                        formula = new int[clauseCount * 3];
                        break;
                    }
                    default:
                    {
                        if (formula == null)
                            throw new InvalidDataException("Wrong source file format.");

                        if (counter == formula.Length)
                            break;

                        var formulaValues = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                                .Select(int.Parse)
                                                .TakeWhile(v => v != 0)
                                                .ToArray();
                        if (formulaValues.Length != 3)
                            throw new InvalidDataException($"Wrong number of clause variables in clause {(counter == 0 ? 1 : counter / 3 + 1)}.");

                        Array.Copy(formulaValues, 0, formula, counter, 3);
                        counter += 3;
                        break;
                    }
                }
            }

            return new BooleanFormula(variableCount, clauseCount, weights, formula);
        }
    }
}