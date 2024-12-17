using System;

public static class MeowProcessor
{
    public static void ProcessMeowables(IEnumerable<IMeowable> meowables)
    {
        foreach (var meowable in meowables)
        {
            meowable.Meow();
        }
    }
}
