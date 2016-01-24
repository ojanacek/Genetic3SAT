using System.Collections;
using System.Linq;

namespace Genetic3SAT
{
    public sealed class Chromosome
    {
        public BitArray Gens { get; set; }
        public int Fitness { get; set; }

        public Chromosome(BitArray gens)
        {
            Gens = gens;
        }

        public override string ToString()
        {
            return $"Fitness: {Fitness}, Gens: {string.Join(" ", Gens.Cast<bool>().Select(b => b ? "1" : "0"))}";
        }
    }
}