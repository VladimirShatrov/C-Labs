using System;
using System.IO;
using System.IO.Pipes;
using System.Xml.Serialization;
using static EnterNum;

public class Files
{
    //4
    public static int BinaryFileNotEvenMultiplyBy2(String path)
    {
        int result = 0;
        using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
        using (BinaryReader reader = new BinaryReader(file))
        {
            while (file.Position <= file.Length - sizeof(int))
            {
                int num = reader.ReadInt32();
                if (num % 2 == 0 && (num / 2) % 2 != 0)
                {
                    result++;
                }
            }
        }

        return result;
    }

    
    public static void BinaryFileWriteNumbers(String path, String verificationFilePath)
    {
        Console.WriteLine("Количество чисел в файле: ");
        int n = enterNum(1);
        Random rnd = new Random();
        using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
        using (BinaryWriter writer = new BinaryWriter(file))
        using (StreamWriter fileWriter = new StreamWriter(verificationFilePath))
        {
            for (int i = 0; i < n; i++)
            {
                int num = rnd.Next(1, 101);
                writer.Write(num);
                fileWriter.Write(num);
                if (i != n - 1)
                    fileWriter.Write("\n");
            }
        }
    }

    [Serializable]
    public struct Toy
    {
        public string Name;
        public double Price;
        public (int, int) AgeGap;
    }

    public static void BinaryFileWriteToys(String path)
    {
        Console.WriteLine("Введите количество игрушек: ");
        int n = enterNum(1);
        using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write)) 
        { 
            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            List<Toy> list = new List<Toy>();
            for (int i = 0; i < n; i++) 
            {
                Toy toy = new Toy();
                Console.WriteLine("Имя игрушки: ");
                while (toy.Name == null)
                {
                    toy.Name = Console.ReadLine();
                }

                Console.WriteLine("Цена игрушки: ");
                toy.Price = enterDoubleNum(0);
                Console.WriteLine("Возрастные границы: "); 
                toy.AgeGap.Item1 = enterNum(0); 
                toy.AgeGap.Item2 = enterNum(toy.AgeGap.Item1);
                list.Add(toy);
            }
            serializer.Serialize(file, list);
        }
    }

    //5
    public static String LessExpensiveToy(String path)
    {
        String nameLessExpensiveToy = "";
        List<Toy> toys;
        double minPrice = double.MaxValue;
        using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            toys = (List<Toy>)serializer.Deserialize(file);
            foreach (Toy toy in toys)
            {
                    if (toy.Price < minPrice)
                    {
                        minPrice = toy.Price;
                        nameLessExpensiveToy = toy.Name;
                    }
            }
        }
        return nameLessExpensiveToy;
    }

    //6
    public static int SquareOfDifferenceOfMaxAndMin(String path)
    {
        int max = int.MinValue, min = int.MaxValue;
        using (FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read))
        using (StreamReader file = new StreamReader(f))
        {
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                if (int.TryParse(line, out int n))
                {
                    if (n > max)
                        max = n;
                    if (n < min)
                        min = n;
                }
            }
        }
        return (max - min) * (max - min);
        
    }

    public static void WriteNumbersToFile(String path)
    {
        Console.WriteLine("Количество чисел в файле: ");
        int n = enterNum(1);
        Random rnd = new Random();
        using (FileStream f = new FileStream(path, FileMode.Create, FileAccess.Write))
        using (StreamWriter file = new StreamWriter(f))
        {
            for (int i = 0; i < n; i++)
            {
                file.Write(rnd.Next(1,101));
                file.Write("\n");
            }

        }
    }
    //7
    public static int SumOfNotEvenElemetns(String path)
    {
        int sum = 0;
        using (StreamReader file = new StreamReader(path))
        {
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                if (int.TryParse(line, out int n))
                {
                    if (n % 2 != 0)
                    {
                        sum += n;
                    }
                }
            }
        }
        return sum;
    }

    //8
    public static void NewFileWithLastSymbolFromEvryLineFromFile(String path, String newFilePath)
    {
        using (FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read))
        using (FileStream output = new FileStream(newFilePath, FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(output))
        using (StreamReader reader = new StreamReader(input))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line != null && line != "")
                    writer.WriteLine(line[line.Length - 1]);
                else
                {
                    writer.WriteLine();
                }
            }
        }
    }
}