using System;
using static MeowProcessor;

public class Programm
{
    public static void Main(string[] args)
    {
        var barsik = new Cat("Барсик");

        barsik.Meow();
        barsik.Meow(3);

        var murzik = new Cat("Мурзик");
        var cats = new List<Cat> { barsik, murzik };
        ProcessMeowables(cats);
        
        var noCats = new List<NoCat> { new NoCat("aaa") };
        //ProcessMeowables(noCats);

        var countedBarsik = new MeowCounter(barsik);
        ProcessMeowables(new List<IMeowable> { countedBarsik });
        Console.WriteLine($"Кот мяукал {countedBarsik.MeowCount} раз.");



        Fraction f0 = new Fraction(0, 1);
        Fraction f1 = new Fraction(1, 2);
        Fraction f2 = new Fraction(-2, 3);

        Console.WriteLine(f0);

        Console.WriteLine("1/2 + 0/1 = " + (f1 + f0));
        Console.WriteLine("1/2 - -2/3 = " + (f1 - f2));
        Console.WriteLine("-2/3 * 0/1 = " + (f2 * f0));
        Console.WriteLine("-2/3 / 1/2 = " + (f2 / f1));

        Console.WriteLine((f0 + f1) * f2 - 5);
    }
}