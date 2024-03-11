using Poker_tournament.Scoring;

namespace Tests;

[TestFixture]
public class WinnerToFinalScoreAlgorithmTest
{
    [Test]
    public void Winner()
    {
        var algorithm = new WinnerToFinalScoreAlgorithm(1000, 10);
        var score1 = algorithm.GetScore(1);
        var score2 = algorithm.GetScore(2);
        
        Assert.Greater(score1, score2);
    }
    
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(20)]
    [TestCase(30)]
    public void WinnerShouldHaveHigherScoreThan2ndAnd3rdPlace(int players)
    {
        var algorithm = new WinnerToFinalScoreAlgorithm(1000, players);
        var score1 = algorithm.GetScore(1);
        var score2 = algorithm.GetScore(2) + algorithm.GetScore(3);
        
        Assert.GreaterOrEqual(score1, score2);
    }
    
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(20)]
    [TestCase(30)]
    public void CheckScoreTable(int players)
    {
        var algorithm = new WinnerToFinalScoreAlgorithm(1000, players);

        var positionScores = algorithm.GetPositionScore();

        foreach (var positionScore in positionScores)
            Console.WriteLine($"{positionScore.Position}: {positionScore.Score}");
    }
    
    
    
    
}

