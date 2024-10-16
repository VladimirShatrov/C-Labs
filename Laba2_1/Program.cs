using System;
class TimeGap
{
    protected bool isStart;
    protected bool isEnd;
    
    public bool IsStart {
        get { return isStart; }
        set { isStart = value; }
    }
    public bool IsEnd {
        set { isEnd = value; }
        get { return isEnd; }
    }

    public override String ToString()
    {
        if (isStart)
        {
            if (isEnd)
            {
                return "time gap start and end";
            }
            else
            {
                return "time gap start";
            }
        } else
        {
            return "time gap not start";
        }
    }

    public TimeGap(TimeGap timeGap)
    {
        this.isStart=timeGap.isStart;
        this.isEnd=timeGap.isEnd;
    }

    public TimeGap()
    {
        this.isStart=false;
        this.isEnd=false;
    }

    public bool IsOver()
    {
        return (isStart && isEnd);
    }

    public TimeGap(bool isStart, bool isEnd)
    {
        this.isStart = isStart;
        this.isEnd = isEnd;
    }
}

class Day : TimeGap
{
    private DayOfWeek dayOfWeek;
    
    public DayOfWeek DayOfWeek
    {
        set { dayOfWeek = value; }
        get { return dayOfWeek; }
    }

    public Day(DayOfWeek dayOfWeek, TimeGap timeGap) : base(timeGap)
    {
        this.dayOfWeek=dayOfWeek;
    }

    public Day() : base()
    {
        this.dayOfWeek = 0;
    }

    public Day nextDay()
    {
        Day next;
        if (dayOfWeek.Equals(DayOfWeek.Saturday))
        {
            next = new Day(DayOfWeek.Sunday, new TimeGap());
        }
        else
        {
            next = new Day(this.dayOfWeek + 1, new TimeGap());
        }
        return next;
    }

    public Day prevDay()
    {
        Day prev;
        if (dayOfWeek.Equals(DayOfWeek.Sunday))
        {
            prev = new Day(DayOfWeek.Saturday, new TimeGap(this.isStart, this.isStart));
        }
        else
        {
            prev = new Day(this.dayOfWeek - 1, new TimeGap(this.isStart, this.isStart));
        }
        return prev;
    }
}

class Test
{
    public static void Main(String[] args)
    {
        int n = EnterNum(0, 1);
        if (n == 0)
        {
            TimeGap[] gaps = {
        new TimeGap(),// false false
        new TimeGap(true, false),// true false
        new TimeGap(new TimeGap(true, false))// true false
        };
            int i = 1;
            foreach (TimeGap gap in gaps)
            {
                Console.WriteLine("{0}-ый gap", i);
                Console.WriteLine(gap.ToString());
                Console.WriteLine("Временной промежуток истек: " + gap.IsOver());
                gap.IsStart = false;
                Console.WriteLine("Временной промежуток закончился: " + gap.IsEnd);
                i++;
            }
            Day day = new Day();
            Day monday = new Day(DayOfWeek.Monday, new TimeGap(true, true));
            day.DayOfWeek = DayOfWeek.Sunday;
            Console.WriteLine(day.ToString() + " это день - " + day.DayOfWeek.ToString());
            Console.WriteLine(monday.ToString() + " это день - " + monday.DayOfWeek.ToString());
        }
        else
        {
            while(true)
            {
                Console.WriteLine("Тесты класса TimeGap:");
                Console.WriteLine("Введите значение полей двух объектов 1 - true, 0 - false");
                
                TimeGap[] gaps = {
                new TimeGap(),
                new TimeGap(EnterNum(0,1) != 0, EnterNum(0,1) != 0),
                new TimeGap(new TimeGap(EnterNum(0,1) != 0, EnterNum(0,1) != 0))
                };
                
                Console.WriteLine("Тест методов ToString и IsOver:");
                int i = 1;
                foreach (TimeGap gap in gaps)
                {
                    Console.WriteLine("{0}-ый gap", i);
                    Console.WriteLine(gap.ToString());
                    Console.WriteLine("Временной промежуток истек: " + gap.IsOver());
                    gap.IsStart = false;
                    Console.WriteLine("Временной промежуток закончился: " + gap.IsEnd);
                    i++;
                }

                Console.WriteLine("Тесты класса Day:");
                Console.WriteLine("Введите значение двух полей 1 - true, 0 - false, и значение дня недели - номер дня недели начиная с 0 - воскресенье заканчивая 6 - суббота:");
                Day dayNext = new Day();
                Day dayPrev = new Day();

                bool start = EnterNum(0, 1) != 0;
                bool over = EnterNum(0, 1) != 0;
                int d = EnterNum(0, 6);
                Day day2 = new Day((DayOfWeek)d, new TimeGap(start, over));

                Console.WriteLine();
                dayNext.DayOfWeek = day2.nextDay().DayOfWeek;
                dayPrev.DayOfWeek = day2.prevDay().DayOfWeek;
                Console.WriteLine(dayNext.ToString() + " это день - " + dayNext.DayOfWeek.ToString());
                Console.WriteLine(day2.ToString() + " это день - " + day2.DayOfWeek.ToString());
                Console.WriteLine(dayPrev.ToString() + " это день - " + dayPrev.DayOfWeek.ToString());

                Console.WriteLine("Хотите выйти из программы? 1 - да, 0 - нет");
                int exit = EnterNum(0, 1);
                if (exit == 1)
                {
                    break;
                }
            }
        }

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
