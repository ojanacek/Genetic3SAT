using System;
using System.IO;
using System.Linq;

namespace Genetic3SAT.WeightsGenerator
{
    class Program
    {
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No directory specified.");
                Console.ReadLine();
                return;
            }
            
            foreach (var file in Directory.EnumerateFiles(args[0], "*.cnf"))
            {
                var lines = File.ReadAllLines(file).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    var line = lines[i];
                    if (line.StartsWith("c"))
                        continue;

                    if (line.StartsWith("p"))
                    {
                        var preambleValues = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                                 .Skip(2)
                                                 .Select(int.Parse)
                                                 .ToArray();
                        int variableCount = preambleValues[0];
                        var weightsLine = $"w {string.Join(" ", GenerateRandomWeights(variableCount))}";

                        lines.Insert(i, weightsLine);
                        File.WriteAllLines(file, lines);
                        break;
                    }
                }
            }
        }

        private static int[] GenerateRandomWeights(int count)
        {
            var weights = new int[count];
            for (int i = 0; i < count; i++)
            {
                weights[i] = random.Next(1, count + 1);
            }
            return weights;
        }
    }
}
