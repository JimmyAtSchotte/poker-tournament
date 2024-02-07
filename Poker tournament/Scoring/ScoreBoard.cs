using System.Text;

namespace Poker_tournament.Scoring;

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
        return AddPlayer(player);
    }
    
    private Player AddPlayer(Player player)
    {
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
                    },
                    TotalScore = player.TotalScore(scoreAlgorithm),
                    IsStillInTheGame = !player.IsOut(_currentRoundIndex)
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

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{_currentRoundIndex}:{_totalRounds}");

        foreach (var player in _players)
            stringBuilder.AppendLine(player.ToString());
        
        var state = stringBuilder.ToString();
        
        var bytes = Encoding.UTF8.GetBytes(state);
        return Convert.ToBase64String(bytes);
        
    }

    public static ScoreBoard FromState(string state)
    {
        var bytes = System.Convert.FromBase64String(state);
        var convertedState = System.Text.Encoding.UTF8.GetString(bytes);
        
        var lines = convertedState.Split(Environment.NewLine);
        var scoreboardConfigArgs = lines[0].Split(":");
        
        var scoreBoard = new ScoreBoard();
        scoreBoard._currentRoundIndex = Convert.ToInt32(scoreboardConfigArgs[0]);
        scoreBoard._totalRounds = Convert.ToInt32(scoreboardConfigArgs[1]);

        for (int i = 1; i < lines.Length; i++)
            if(!string.IsNullOrEmpty(lines[i]))
                scoreBoard.AddPlayer(Player.FromState(lines[i]));

        return scoreBoard;

    }
}