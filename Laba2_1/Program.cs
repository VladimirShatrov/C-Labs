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
        Day nextDay = new Day(this.dayOfWeek + 1, new TimeGap());
        return nextDay;
    }

    public Day prevDay()
    {
        Day prevDay = new Day(this.dayOfWeek - 1, new TimeGap(this.isStart, this.isStart));
        return prevDay;
    }
}

class Test
{
    public static void Main(String[] args)
    {
        TimeGap[] gaps = {
        new TimeGap(),// false false
        new TimeGap(true, false),// true false
        new TimeGap(new TimeGap(true, false))// true false
        };
        foreach(TimeGap gap in gaps)
        {
            Console.WriteLine(gap.ToString());
            Console.WriteLine("Is time gap over: " + gap.IsOver());
            gap.SetIsStart(false);
            Console.WriteLine("gap is end: " + gap.GetIsEnd());
        }
        Day day = new Day();
        Day monday = new Day(DayOfWeek.Monday, new TimeGap(true, true));
        day.SetDayOfWeek(DayOfWeek.Sunday);
        Console.WriteLine(day.ToString() + " this day is - " + day.GetDayOfWeek().ToString());
        Console.WriteLine(monday.ToString() + " this day is - " + monday.GetDayOfWeek().ToString());


    }
}