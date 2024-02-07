namespace Tests;

public record ScoreRow
{
    public Player Player { get; init; }
    public int Position { get; init; }
    public int[] Scores { get; init; }
}