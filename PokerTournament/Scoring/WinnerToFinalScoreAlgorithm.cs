namespace Poker_tournament.Scoring;

public class WinnerToFinalScoreAlgorithm : IScoreAlgorithm
{
    private readonly IDictionary<int, int> _scoreTable;

    public WinnerToFinalScoreAlgorithm(int baseScoreCount, int players)
    {
        _scoreTable = CreateScoreTable(baseScoreCount, players);
    }

    private static Dictionary<int, int> CreateScoreTable(int baseScoreCount, int players)
    {
        var scoreTable = new Dictionary<int, int>();
        scoreTable.Add(0, baseScoreCount);

        var decrementDecrease = Math.Max(1 - (players * 0.08), 0.05d);
        var score = (double)baseScoreCount;
        var decrement = baseScoreCount - (baseScoreCount * 0.51); 
        var minimumDecrement = decrement / (players - 1);
        
        for (int i = 1; i < players; i++)
        {
            score -= decrement;
            
            if(score < minimumDecrement)
                break;

            scoreTable.Add(i, (int)Math.Round(score / 10) * 10);
            decrement = Math.Max(decrement * decrementDecrease, minimumDecrement);
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

public record PositionScore
{
    public int Position { get; init; }
    public int Score { get; init; }
    
}
