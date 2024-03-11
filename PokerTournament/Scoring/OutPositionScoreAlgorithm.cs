namespace Poker_tournament.Scoring;

public class OutPositionScoreAlgorithm : IScoreAlgorithm
{
    private readonly int _players;

    public OutPositionScoreAlgorithm(int players)
    {
        _players = players;
    }

    public int GetScore(int position) => _players - position + 1;
    public IDictionary<int, int> GetScoreTable()
    {
        throw new NotImplementedException();
    }
}