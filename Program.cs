using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using static Laba4;

public class Program
{
    private struct Point
    {
        private int x;
        private int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public override string ToString()
        {
            return string.Format("x: {0}, y: {1}", x, y);
        }
    }

    private struct Elective
    {
        private string name;
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Elective (string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Elective other)
            {
                return string.Equals(name, other.name);
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (name == null)
                return 0;
            return name.GetHashCode();
        }
    }

    public static void Main(string[] args)
    {
        //1
        List<int> numList = new List<int> { 1, 1, 1, 2, 3, 1, 2, 3, 8, 0, 0, 1, 2, 99, 99, 1, 2, 3 };
        List<Point> pointList = new List<Point> { new Point(1, 1), new Point(0, 0), new Point(0, 0) };
        List<string> stringList = new List<string> { "aboba", "taa", "taa", "taa" };

        numList = DeleteRepeatingNumbers(numList);
        pointList = DeleteRepeatingNumbers(pointList);
        stringList = DeleteRepeatingNumbers(stringList);

        foreach (int i in numList)
        {
            Console.WriteLine(i);
        }
        Console.WriteLine();
        foreach (Point i in pointList)
        {
            Console.WriteLine(i);
        }
        Console.WriteLine();
        foreach (string i in stringList)
        {
            Console.WriteLine(i);
        }

        //2
        LinkedList<int> linkedNumList = LinkedListFromList(numList);
        LinkedList<Point> linkedPointList = LinkedListFromList(pointList);
        LinkedList<string> linkedStringList = LinkedListFromList(stringList);
        foreach (int i in linkedNumList)
        {
            var currentNode = linkedNumList.Find(i);
            Console.WriteLine("prev: {0}, value: {1}, next: {2}",
            currentNode?.Previous == null ? "null" : currentNode.Previous.Value.ToString(),
            currentNode?.Value.ToString() ?? "null",
            currentNode?.Next == null ? "null" : currentNode.Next.Value.ToString());
        }
        Console.WriteLine();
        foreach (Point i in linkedPointList)
        {
            var currentNode = linkedPointList.Find(i);
            Console.WriteLine("prev: {0}, value: {1}, next: {2}",
            currentNode?.Previous == null ? "null" : currentNode.Previous.Value.ToString(),
            currentNode?.Value.ToString() ?? "null",
            currentNode?.Next == null ? "null" : currentNode.Next.Value.ToString());
        }
        Console.WriteLine();
        foreach (string i in linkedStringList)
        {
            var currentNode = linkedStringList.Find(i);
            Console.WriteLine("prev: {0}, value: {1}, next: {2}",
            currentNode?.Previous == null ? "null" : currentNode.Previous.Value.ToString(),
            currentNode?.Value.ToString() ?? "null",
            currentNode?.Next == null ? "null" : currentNode.Next.Value.ToString());
        }

        //3
        HashSet<string> electives = new HashSet<string> { "1", "2", "3", "4", "5", "6" };
        HashSet<Elective> electivesObjects = new HashSet<Elective> { new Elective("математика"), new Elective("физика"),
            new Elective("робототехника"), new Elective("не придумал"), new Elective("гооол") };

        Dictionary<string, HashSet<string>> studentesElectives = new Dictionary<string, HashSet<string>>();
        studentesElectives.Add("Иван", new HashSet<string> { "3", "2", "1" });
        studentesElectives.Add("Максим", new HashSet<string> { "3" });
        studentesElectives.Add("Петр", new HashSet<string> { "4", "3" });

        Dictionary<string, HashSet<Elective>> studentesElectiveObj = new Dictionary<string, HashSet<Elective>>();
        studentesElectiveObj.Add("Иван", new HashSet<Elective> { new Elective("математика"), new Elective("физика"), new Elective("робототехника") });
        studentesElectiveObj.Add("Максим", new HashSet<Elective>{ new Elective("математика") });
        studentesElectiveObj.Add("Петр", new HashSet<Elective> { new Elective("не придумал"), new Elective("математика") });

        HashSet<string> allString = AllStudentsElective(studentesElectives);
        HashSet<string> anyString = AnyStudentsElective(studentesElectives);
        HashSet<string> noString = NoStudentsElective(electives, studentesElectives);

        Console.WriteLine("Факльтатива, которые посещают все студенты: ");
        foreach (var i in allString) Console.WriteLine(i);
        Console.WriteLine("Факльтатива, которые посещает хотя бы один студент: ");
        foreach (var i in anyString) Console.WriteLine(i);
        Console.WriteLine("Факльтатива, которые не посещают студенты: ");
        foreach (var i in noString) Console.WriteLine(i);

        Console.WriteLine();

        HashSet<Elective> allElective = AllStudentsElective(studentesElectiveObj);
        HashSet<Elective> anyElective = AnyStudentsElective(studentesElectiveObj);
        HashSet<Elective> noElective = NoStudentsElective(electivesObjects, studentesElectiveObj);

        Console.WriteLine("Факльтатива, которые посещают все студенты: ");
        foreach (var i in allElective) Console.WriteLine(i);
        Console.WriteLine("Факльтатива, которые посещает хотя бы один студент: ");
        foreach (var i in anyElective) Console.WriteLine(i);
        Console.WriteLine("Факльтатива, которые не посещают студенты: ");
        foreach (var i in noElective) Console.WriteLine(i);

        Console.WriteLine();

        //4
        LettersInWordsWithNotEvenNumbers("notExistsFilePath.taa");
        LettersInWordsWithNotEvenNumbers("1.txt");

        //5
        int n = WriteToFile("2.xml");
        GasStationsLessExpensiveGasoline("notExistsFilePath.xml", n);
        GasStationsLessExpensiveGasoline("2.xml", n);
    }
}