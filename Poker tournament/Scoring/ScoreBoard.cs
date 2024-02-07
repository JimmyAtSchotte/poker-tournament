namespace Tests;

public class ScoreBoard
{
    private readonly IList<Player> _players;
    private int _currentRoundIndex = 0;
    private int _totalRounds = 2;
    
    public ScoreBoard()
    {
        _players = new List<Player>();
    }
    
    public Player AddPlayer(string name)
    {
        var player = new Player(name);
        _players.Add(player);
        return player;
    }
    public IEnumerable<ScoreRow> ScoreTable(IScoreAlgorithm scoreAlgorithm)
    {
        var currentPosition = 1;
        var nextOutPosition = _players.Count(x => x.IsOut(_currentRoundIndex)) + 1;
        var scoreGrouped = _players.GroupBy(x =>
            x.TotalScore(scoreAlgorithm) + (x.IsOut(_currentRoundIndex) ? 0 : scoreAlgorithm.GetScore(nextOutPosition))
        ).OrderByDescending(x => x.Key);
        
        foreach (var group in scoreGrouped)
        {
            foreach (var player in group)
            {
                yield return new ScoreRow()
                {
                    Player = player,
                    Position = currentPosition,
                    Scores = new int[]
                    {
                        player.RoundScore(scoreAlgorithm, 0), 
                        player.RoundScore(scoreAlgorithm, 1), 
                    }
                };
            }
            
            currentPosition += group.Count();
        }
    }

    public void Out(Player player)
    {
        var position = _players.Count(x => x.IsOut(_currentRoundIndex)) + 1;
        player.Out(position, _currentRoundIndex);

        if (_players.Count(x => x.IsOut(_currentRoundIndex)) == _players.Count() - 1)
        {
            _players.First(x => !x.IsOut(_currentRoundIndex)).Out(_players.Count(), _currentRoundIndex);
            
            if(_currentRoundIndex < _totalRounds-1)
                _currentRoundIndex += 1;
        }
    }
}

public interface IScoreAlgorithm
{
    int GetScore(int position);
}

public class OutPositionScoreAlgorithm : IScoreAlgorithm
{
    public int GetScore(int position) => position;
}

