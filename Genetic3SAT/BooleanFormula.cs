namespace Genetic3SAT
{
    public sealed class BooleanFormula
    {
        public int VariableCount { get; }
        public int ClauseCount { get; }

        public int[] Weights { get; }
        public int[] Formula { get; }

        public BooleanFormula(int variableCount, int clauseCount, int[] weights, int[] formula)
        {
            VariableCount = variableCount;
            ClauseCount = clauseCount;
            Weights = weights;
            Formula = formula;
        }
    }
}