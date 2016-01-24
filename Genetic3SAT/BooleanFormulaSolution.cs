namespace Genetic3SAT
{
    public sealed class BooleanFormulaSolution
    {
        public Chromosome Chromosome { get; }
        public int GenerationCount { get; }

        public BooleanFormulaSolution(Chromosome chromosome, int generationCount)
        {
            Chromosome = chromosome;
            GenerationCount = generationCount;
        }
    }
}