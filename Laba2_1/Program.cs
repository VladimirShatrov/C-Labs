//1_10
class TimeGap
{
    protected bool isStart;
    protected bool isEnd;

    public bool GetIsStart()
    {
        return isStart;
    }
    public bool GetIsEnd()
    {
        return isEnd;
    }
    public void SetIsStart(bool isStart)
    {
        this.isStart = isStart;
    }
    public void SetIsEnd(bool isEnd)
    {
        this.isEnd = isEnd;
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

    public void SetDayOfWeek(DayOfWeek dayOfWeek)
    {
        this.dayOfWeek = dayOfWeek;
    }

    public DayOfWeek GetDayOfWeek()
    {
        return this.dayOfWeek;
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
                gap.SetIsStart(false);
                Console.WriteLine("Временной промежуток закончился: " + gap.GetIsEnd());
                i++;
            }
            Day day = new Day();
            Day monday = new Day(DayOfWeek.Monday, new TimeGap(true, true));
            day.SetDayOfWeek(DayOfWeek.Sunday);
            Console.WriteLine(day.ToString() + " это день - " + day.GetDayOfWeek().ToString());
            Console.WriteLine(monday.ToString() + " это день - " + monday.GetDayOfWeek().ToString());
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
                    gap.SetIsStart(false);
                    Console.WriteLine("Временной промежуток закончился: " + gap.GetIsEnd());
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
                dayNext.SetDayOfWeek(day2.nextDay().GetDayOfWeek());
                dayPrev.SetDayOfWeek(day2.prevDay().GetDayOfWeek());
                Console.WriteLine(dayNext.ToString() + " это день - " + dayNext.GetDayOfWeek().ToString());
                Console.WriteLine(day2.ToString() + " это день - " + day2.GetDayOfWeek().ToString());
                Console.WriteLine(dayPrev.ToString() + " это день - " + dayPrev.GetDayOfWeek().ToString());

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