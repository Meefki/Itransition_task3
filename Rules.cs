using System.Data;

namespace task3;

internal class Rules
{
    public List<string> Moves { get; init; }
    public int[,] Rule { get; init; }

    public Rules(string[] moves)
    {
        Moves = moves.ToList();
        Rule = CalculateRules();
    }

    public int CompMove()
    {
        Random random = new Random();
        int index = random.Next(Moves.Count - 1);

        return index;
    }

    public int CalculateWinner(int compMove, int userMove) => Rule[compMove, userMove];

    private int[,] CalculateRules()
    {
        int[,] rules = new int[Moves.Count, Moves.Count];

        for (int i = 0; i < Moves.Count; i++)
        {
            rules[i, i] = 0;

            for (int j = 1; j <= Moves.Count / 2; j++)
            {
                int l = CalcLoseIndex(i, j);
                int w = CalcWinIndex(i, j);

                rules[i, l] = 1;
                rules[i, w] = -1;

                rules[l, i] = -1;
                rules[w, i] = 1;
            }
        }

        return rules;
    }

    private int CalcLoseIndex(int i, int j) => (i + j) % Moves.Count;
    private int CalcWinIndex(int i, int j) => i - j < 0 ? Moves.Count + i - j : i - j;
}
