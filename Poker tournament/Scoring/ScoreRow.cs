namespace Poker_tournament.Scoring;
public record ScoreRow
{
    public Player Player { get; init; }
    public int Position { get; init; }
    public int[] Scores { get; init; }
    public int TotalScore { get; set; }
    public bool IsStillInTheGame { get; set; }
}