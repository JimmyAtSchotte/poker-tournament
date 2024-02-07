namespace Tests;

public class Tests
{
    private readonly IScoreAlgorithm _scoreAlgorithm = new OutPositionScoreAlgorithm();
    
    [Test]
    public void ListOnePlayer()
    {
        var scoreBoard = new ScoreBoard();
        scoreBoard.AddPlayer("P1");
        var scoreTable = scoreBoard.ScoreTable(_scoreAlgorithm).ToArray();

        Assert.AreEqual("P1", scoreTable[0].Player.Name);
    }
    
    [Test]
    public void ListTowPlayersWithSameScore()
    {
        var scoreBoard = new ScoreBoard();
        scoreBoard.AddPlayer("P1");      
        scoreBoard.AddPlayer("P2");

        var scoreTable = scoreBoard.ScoreTable(_scoreAlgorithm).ToArray();

        Assert.AreEqual(2, scoreTable.Length);
        Assert.AreEqual(1, scoreTable[0].Position);
        Assert.AreEqual(1, scoreTable[1].Position);
    }
    
    [Test]
    public void OrderByPotentialScore()
    {
        var scoreBoard = new ScoreBoard();
        var p1 = scoreBoard.AddPlayer("P1");      
        var p2 = scoreBoard.AddPlayer("P2");
        var p3 = scoreBoard.AddPlayer("P3");

        scoreBoard.Out(p1);

        var scoreTable = scoreBoard.ScoreTable(_scoreAlgorithm).ToArray();

        Assert.AreEqual(1, scoreTable[0].Position);
        Assert.AreEqual(1, scoreTable[1].Position);
        Assert.AreEqual(3, scoreTable[2].Position);
        Assert.AreEqual(p2.Name, scoreTable[0].Player.Name);
        Assert.AreEqual(p3.Name, scoreTable[1].Player.Name);
        Assert.AreEqual(p1.Name, scoreTable[2].Player.Name);
    }
    
    [Test]
    public void RoundCompletesOnNextToLastOut()
    {
        var scoreBoard = new ScoreBoard();
        var p1 = scoreBoard.AddPlayer("P1");      
        var p2 = scoreBoard.AddPlayer("P2");

        scoreBoard.Out(p1);

        var scoreTable = scoreBoard.ScoreTable(_scoreAlgorithm).ToArray();
        
        Assert.AreEqual(true, scoreTable[0].Player.IsOut(0));        
        Assert.AreEqual(true, scoreTable[1].Player.IsOut(0));
    }
    
    [Test]
    public void MultipleRounds()
    {
        var scoreBoard = new ScoreBoard();
        var p1 = scoreBoard.AddPlayer("P1");      
        var p2 = scoreBoard.AddPlayer("P2");

        scoreBoard.Out(p1);
        scoreBoard.Out(p2);

        var scoreTable = scoreBoard.ScoreTable(_scoreAlgorithm).ToArray();
        
        Assert.AreEqual(1, scoreTable[0].Position);
        Assert.AreEqual(1, scoreTable[1].Position);
        Assert.AreEqual(1, scoreTable[0].Scores[0]);
        Assert.AreEqual(2, scoreTable[0].Scores[1]);
        Assert.AreEqual(2, scoreTable[1].Scores[0]);
        Assert.AreEqual(1, scoreTable[1].Scores[1]);
    }

}