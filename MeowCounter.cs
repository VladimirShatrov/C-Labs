using System;

public class MeowCounter : IMeowable
{
    private readonly IMeowable _meowable;
    public int MeowCount { get; private set; } = 0;

    public MeowCounter(IMeowable meowable)
    {
        _meowable = meowable;
    }

    public void Meow()
    {
        MeowCount++;
        _meowable.Meow();
    }
}
