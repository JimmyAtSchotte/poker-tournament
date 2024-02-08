using Poker_tournament.Scoring;

namespace Tests;

public class MementoTest
{
    [Test]
    public void Rollback()
    {
        var memento = new Memento<string>();
        memento.Push("1");
        memento.Push("2");
        var result = memento.Undo();

        Assert.AreEqual("1", result);
    }

    [Test]
    public void PushUndoPushUndo()
    {
        var memento = new Memento<string>();
        memento.Push("1");
        memento.Push("2");
        memento.Undo();
        memento.Push("3");
        var result = memento.Undo();

        Assert.AreEqual("1", result);
    }
    
    [Test]
    public void Redo()
    {
        var memento = new Memento<string>();
        memento.Push("1");
        memento.Push("2");
        memento.Undo();
        var result = memento.Redo();

        Assert.AreEqual("2", result);
    }
    
    [Test]
    public void UndoOutOfBounce()
    {
        var memento = new Memento<string>();
        var result = memento.Undo();

        Assert.IsNull(result);
    }
    
    [Test]
    public void UndoOutOfBouncePickFirstItem()
    {
        var memento = new Memento<string>();
        memento.Push("1");
        memento.Undo();
        var result = memento.Undo();

        Assert.AreEqual("1", result);
    }
    
    [Test]
    public void RedoOutOfBounce()
    {
        var memento = new Memento<string>();
        var result = memento.Redo();

        Assert.IsNull(result);
    }
    
    [Test]
    public void RedoOutOfBouncePickLastItem()
    {
        var memento = new Memento<string>();
        memento.Push("1");
        var result = memento.Redo();

        Assert.AreEqual("1", result);
    }
}