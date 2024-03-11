namespace Poker_tournament.Scoring;

public class PrizeTable
{
    private readonly decimal _buyIn;

    public PrizeTable(decimal buyIn)
    {
        _buyIn = buyIn;
    }

    public IEnumerable<PrizeRow> GetPrizeTable(int players)
    {
        var totalPot = _buyIn * players;
        
        var table = new List<PrizeRow>();

        if (players == 2)
        {
            table.Add(new PrizeRow(1, totalPot));
            return table;
        }

        if (players <= 6)
        {
            table.Add(new PrizeRow(1, totalPot * 0.60m));
            table.Add(new PrizeRow(2, totalPot * 0.40m));
            return table;
        }

        table.Add(new PrizeRow(1, totalPot * 0.60m));
        table.Add(new PrizeRow(2, totalPot * 0.25m));
        table.Add(new PrizeRow(3, totalPot * 0.15m));

        return table;
    }
}

public class PrizeRow
{
    public int Position { get; set; }
    public decimal Prize { get; set; }

    public PrizeRow(int position, decimal prize)
    {
        Position = position;
        Prize = prize;
    }
    
    
}