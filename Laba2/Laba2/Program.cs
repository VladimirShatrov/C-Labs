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
        int n = EnterNum(0, 1);
        if (n == 0)
        {
            Triangle t = new Triangle(3, 4, 5);
            Console.WriteLine("Триугольник со сторнами 3 4 5 существует: " + t.isExist());
            Triangle t2 = new Triangle(1, 0, 0);
            Console.WriteLine("Триугольник со сторнами 1 0 0 существует: " + t2.isExist());

            Console.WriteLine("Площадь треугольника: " + -t);
            TestDoubleCast(t);
            TestBoolCast((bool)t);

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
        else
        {
            while (true)
            {
                Console.WriteLine("Тест на сущесвтования треугольника:");
                Console.WriteLine("Введите длины трех сторон");
                Triangle t = new Triangle();
                int a, b, c;
                a = EnterNum();
                b = EnterNum();
                c = EnterNum();

                t.SetA(a);
                t.SetB(b);
                t.SetC(c);

                Console.WriteLine("Триугольник со сторнами {0} {1} {2} существует: " + t.isExist(), a, b, c);

                Console.WriteLine("Тест на нахождение площади:");
                while(!t.isExist())
                {
                    Console.WriteLine("Треугольника не существует, введите новые значения длин: ");
                    t.SetA(EnterNum());
                    t.SetB(EnterNum());
                    t.SetC(EnterNum());
                }
                Console.WriteLine("Площадь труголника = {0}", -t);

                Console.WriteLine("Тест на приведение типов:");
                Console.WriteLine("Неявное:");
                TestDoubleCast(t);
                Console.WriteLine("Явное:");
                TestDoubleCast((double)t);

                Console.WriteLine("Тест операторов сравнения:");

                Console.WriteLine("Введите длины сторон второго треугольника:");
                a = EnterNum();
                b = EnterNum();
                c = EnterNum();
                Triangle t2 = new Triangle(a, b, c);
                if (t > t2)
                {
                    Console.WriteLine("Первый треугольник больше второго.");
                }
                if (t < t2)
                {
                    Console.WriteLine("Первый треугольник меньше второго.");
                }
            }
        }
    }

    private static void TestDoubleCast(double x)
    {
        Console.WriteLine("Goooal " +  x);
    }

    private static void TestBoolCast(bool x)
    {
        Console.WriteLine("Goooal " + x);
    }

    private static int EnterNum(int left, int right)
    {
        int n;
        Console.WriteLine("Введите число от {0} до {1}: ", left, right);
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out n) && n <= right && n >= left) return n;

            else
            {
                Console.WriteLine("неверный ввод");
                Console.WriteLine("введите число от {0} до {1} повторно: ", left, right);
            }

        }
    }

    private static int EnterNum()
    {
        int n;
        Console.WriteLine("Введите число: ");
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out n)) return n;

            else
            {
                Console.WriteLine("неверный ввод");
                Console.WriteLine("введите число повторно: ");
            }

        }
    }

}