using System;
using System.Text;

public class Fraction : ICloneable, IReal
{
    private int _numerator;
    private int _denominator;
    private double _double;

    public int Numerator
    {
        get
        {
            return _numerator;
        }    
        set
        {
            _numerator = value;
        }
    }
    public int Denominator
    {
        get
        {
            return _denominator;
        }
        set
        {
            if (value <= 0) throw new ArgumentException("Знаменатель не может быть отрицательным  или равным 0.");
            else _denominator = value;
        }
    }

    public Fraction(int numerator, int denominator)
    {
        Numerator = numerator;
        if (denominator <= 0)
        {
            throw new ArgumentException("Знаменатель не может быть отрицательным или равным 0.");
        }
        Denominator = denominator;
    }

    public override string ToString()
    {
        return $"{Numerator}/{Denominator}";
    }

    public static Fraction operator +(Fraction f1, Fraction f2)
    {
        int denominatorF3;
        int numeratorF3;

        if (f1._denominator == f2._denominator)
        {
            denominatorF3 = f1._numerator;
            numeratorF3 = f1._numerator + f2._numerator;
        }
        else
        {
            denominatorF3 = f2._denominator * f1._denominator;
            numeratorF3 = (f1._numerator * f2._denominator) + (f2._numerator * f1._denominator);
        }

        return new Fraction(numeratorF3, denominatorF3);
    }

    public static Fraction operator -(Fraction f1, Fraction f2)
    {
        int denominatorF3;
        int numeratorF3;

        if (f1._denominator == f2._denominator)
        {
            denominatorF3 = f1._numerator;
            numeratorF3 = f1._numerator - f2._numerator;
        }
        else
        {
            denominatorF3 = f2._denominator * f1._denominator;
            numeratorF3 = (f1._numerator * f2._denominator) - (f2._numerator * f1._denominator);
        }

        return new Fraction(numeratorF3, denominatorF3);
    }

    public static Fraction operator *(Fraction f1, Fraction f2)
    {
        return new Fraction(
                f1._numerator * f2._numerator,
                f1._denominator * f2._denominator
            );
    }

    public static Fraction operator /(Fraction f1, Fraction f2)
    {
        if (f2._numerator == 0)
        {
            throw new ArgumentException("Нельзя делить на 0.");
        }
        Fraction newF2 = new Fraction(f2._numerator, f2._denominator);
        if (newF2._numerator <= 0)
        {
            int t = -newF2._numerator;
            newF2._numerator = -newF2._denominator;
            newF2._denominator = t;
        }
        else
        {
            int t = newF2._numerator;
            newF2._numerator = newF2._denominator;
            newF2._denominator = t;
        }

        return f1 * newF2;
    }

    public static Fraction operator +(Fraction f1, int num)
    {
        Fraction f2 = new Fraction(num, 1);
        return f1 + f2;
    }

    public static Fraction operator -(Fraction f1, int num)
    {
        Fraction f2 = new Fraction(num, 1);
        return f1 - f2;
    }

    public static Fraction operator *(Fraction f1, int num)
    {
        Fraction f2 = new Fraction(num, 1);
        return f1 * f2;
    }

    public static Fraction operator /(Fraction f1, int num)
    {
        Fraction f2 = new Fraction(num, 1);
        return f1 / f2;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Fraction && obj != null)
        {
            Fraction f1 = obj as Fraction;
            return f1._numerator == _numerator && f1._denominator == _denominator;
        }
        return false;
    }

    public Fraction Clone()
    {
        return new Fraction(_numerator, _denominator);
    }

    public double GetDouble()
    {
        double value = _numerator / _denominator;
        _double = value;
        return value;
    }

    public int GetNumerator()
    {
        return _numerator;
    }

    public int GetDenominator()
    {
        return _denominator;
    }
}