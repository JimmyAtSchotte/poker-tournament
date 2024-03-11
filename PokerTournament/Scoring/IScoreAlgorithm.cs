using System.Collections;

namespace Poker_tournament.Scoring;

public interface IScoreAlgorithm
{
    int GetScore(int position);
    IDictionary<int, int> GetScoreTable();
}