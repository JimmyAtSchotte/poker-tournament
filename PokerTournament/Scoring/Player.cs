﻿namespace Poker_tournament.Scoring;

public class Player
{
    public int[] _position;

    public Player(string name)
    {
        Name = name;
        _position = new int[2];
    }
    
    public string Name { get; set; }
    
    public int TotalScore(IScoreAlgorithm scoreAlgorithm)
    {
        return _position.Sum(scoreAlgorithm.GetScore);
    }

    public void Out(int position, int round)
    {
        _position[round] = position;
    }
    
    public int RoundScore(IScoreAlgorithm scoreAlgorithm, int round)
    {
        return scoreAlgorithm.GetScore(_position[round]);
    }

    public bool IsOut(int index) => _position[index] > 0;
    
    
    public override string ToString()
    {
        return $"{Name}:{string.Join(',', _position)}";
    }

    public static Player FromState(string state)
    {
        var args = state.Split(":");
        var position = args[1].Split(',').Select(x => Convert.ToInt32(x)).ToArray();
        return new Player(args[0])
        {
            _position = position
        };
    }
}