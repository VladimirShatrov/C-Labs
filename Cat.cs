using System;

public class Cat : IMeowable
{
    public string Name { get; set; }

    public Cat(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return $"кот: {Name}";
    }

    public void Meow()
    {
        Console.WriteLine($"{Name}: мяу!");
    }

    public void Meow(int n)
    {
        Console.Write($"{Name}: ");
        for (int i = n; i > 0; i--)
        {
            if (i == 1)
            {
                Console.WriteLine("мяу!");
            }
            else
            {
                Console.Write("мяу-");
            }
        }
    }
}
