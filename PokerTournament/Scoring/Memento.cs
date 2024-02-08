namespace Poker_tournament.Scoring;

public class Memento<T>
{
    private IList<T> _mementos;
    private int _index;

    public Memento()
    {
        _mementos = new List<T>();
        _index = 0;
    }

    public void Push(T state)
    {
        if (_index < _mementos.Count - 1)
            _mementos = _mementos.Take(_index + 1).ToList();

        _mementos.Add(state);
        _index = _mementos.Count - 1;
    }
    
    public T Undo()
    {
        if (_index == 0)
            return _mementos.FirstOrDefault();
        
        _index -= 1;
        
        return _mementos.ElementAt(_index);
    }

    public T Redo()
    {
        if (_mementos.Count == 0 || _index == _mementos.Count - 1)
            return _mementos.LastOrDefault();
        
        _index += 1;
        return _mementos.ElementAt(_index);
    }
}