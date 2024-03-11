namespace Poker_tournament.Scoring;

public class ProcentProgressiveAlgorithm : IScoreAlgorithm
{
    private readonly IDictionary<int, int> _scoreTable;

    public ProcentProgressiveAlgorithm(int baseScoreCount, int players)
    {
        _scoreTable = CreateScoreTable(baseScoreCount, players);
    }

    private static Dictionary<int, int> CreateScoreTable(int baseScoreCount, int players)
    {
        var scoreTable = new Dictionary<int, int>();
        var current = baseScoreCount * 0.1;
        

        for (int i = players-1; i >= 0; i--)
        {
            scoreTable.Add(i, (int)current);

            current *= 1.5;
        }
        
        return scoreTable;
    }
    
   

    public int GetScore(int position)
    {
        return _scoreTable.TryGetValue(position -1, out var score) ? score : 0;
    }

    public IDictionary<int, int> GetScoreTable()
    {
        return _scoreTable.ToDictionary(k => k.Key + 1, v => v.Value);
    }

    public IEnumerable<PositionScore> GetPositionScore()
    {
        foreach (var item in _scoreTable)
        {
            yield return new PositionScore()
            {
                Position = item.Key + 1,
                Score = item.Value
            };
        }
    }
}


