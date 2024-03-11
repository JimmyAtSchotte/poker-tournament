using Poker_tournament.Scoring;

namespace Tests;

[TestFixture]
public class PrizeTableTests
{
    [Test]
    public void TwoPlayers()
    {
        var prizeTable = new PrizeTable(250);
        var result = prizeTable.GetPrizeTable(2);
        
        Assert.That(result.FirstOrDefault().Prize, Is.EqualTo(500));
    }
    
    [Test]
    public void ThreePlayers()
    {
        var prizeTable = new PrizeTable(250);
        var result = prizeTable.GetPrizeTable(3);
        
        Assert.That(result.First(p => p.Position == 1).Prize, Is.EqualTo(450m));
        Assert.That(result.First(p => p.Position == 2).Prize, Is.EqualTo(300m));
   
    }
    
    [Test]
    public void SevenPlayers()
    {
        var prizeTable = new PrizeTable(250);
        var result = prizeTable.GetPrizeTable(7);
        
        Assert.That(result.First(p => p.Position == 3).Prize, Is.GreaterThan(250m));
   
    }
    
   
}

