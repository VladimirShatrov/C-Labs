using System;

public class EnterNum
{
    public static int[] enterArray()
    {
        Console.WriteLine("Ввод размерности массива.");
        int x = enterNum(1);
        int[] arr = new int[x];
        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine("Ввод {0}-ого элемента массива: ", i);
            arr[i] = enterNum();
        }
        return arr;
    }

    public static int enterNum(int left, int right)
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
    public static int enterNum(int left)
    {
        int n;
        Console.WriteLine("Введите число от {0}: ", left);
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out n) && n >= left) return n;

            else
            {
                Console.WriteLine("неверный ввод");
                Console.WriteLine("введите число от {0} повторно: ", left);
            }

        }
    }

    public static int enterNumNotIncludingGap(int left, int right)
    {
        int n;
        Console.WriteLine("Введите число до {0} или от {1}: ", left, right);
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out n) && (n >= right || n <= left)) return n;

            else
            {
                Console.WriteLine("неверный ввод");
                Console.WriteLine("введите число до {0} или от {1} повторно: ", left, right);
            }

        }
    }
    public static int enterNum()
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

    public static double enterDoubleNum(double left)
    {
        double n;
        Console.WriteLine("Введите число от {0}: ", left);
        while (true)
        {
            var input = Console.ReadLine();
            if (double.TryParse(input, out n) && n >= left) return n;

            else
            {
                Console.WriteLine("неверный ввод");
                Console.WriteLine("введите число от {0} повторно: ", left);
            }

        }
    }
}
