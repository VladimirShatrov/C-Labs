class Triangle
{
    private double a;
    private double b;
    private double c;

    public double GetA()
    {
        return a;
    }
    public double GetB()
    {
        return b;
    }
    public double GetC()
    {
        return c;
    }
    public void SetA(double x)
    {
        a = x;
    }
    public void SetB(double x)
    {
        b = x;
    }
    public void SetC(double x)
    {
        c = x;
    }

    public Triangle()
    {
        a=0;
        b=0;
        c=0;
    }
    public Triangle(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public bool isExist()
    {
        return ((a + b > c) && (a + c > b) && (b + c > a)) && (a > 0 && b > 0 && c > 0);
    }


    public static double operator -(Triangle t)
    {
        double p = (t.a + t.b + t.c) / 2;
        return Math.Sqrt(p * (p - t.a) * (p - t.b) * (p - t.c));
    }

    public static implicit operator double(Triangle t) => t.a + t.b + t.c;
    public static explicit operator bool(Triangle t) => t.isExist();

    public static bool operator <(Triangle t, Triangle t2) => -t < -t2;
    public static bool operator >(Triangle t, Triangle t2) => -t > -t2;
}


class Test
{
    public static void Main(string[] args)
    {
        Triangle t = new Triangle(3, 4, 5);
        Console.WriteLine("Триугольник со сторнами 3 4 5 существует: " + t.isExist());
        Triangle t2 = new Triangle(1, 0, 0);
        Console.WriteLine("Триугольник со сторнами 1 0 0 существует: " + t2.isExist());

        Console.WriteLine("Площадь треугольника: " + -t);
        testDoubleCast(t);
        testBoolCast((bool)t);

        Triangle t3 = new Triangle(5, 6, 7);
        if (t3 > t)
        {
            Console.WriteLine("третий треугольник больше первого.");
        }
        if (t3 < t)
        {
            Console.WriteLine("третий треугольник меньше первого.");
        }
    }

    public static void testDoubleCast(double x)
    {
        Console.WriteLine("Goooal " +  x);
    }

    public static void testBoolCast(bool x)
    {
        Console.WriteLine("Goooal " + x);
    }

}