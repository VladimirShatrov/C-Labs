using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

public class Laba4
{
	public static List<T> DeleteRepeatingNumbers<T>(List<T> inputList)
	{
		List<T> outputList = new List<T> { inputList[0] };
		for (int i = 1; i < inputList.Count; i++)
		{
			//if (!EqualityComparer<T>.Default.Equals(inputList[i], inputList[i - 1]))
			if (!inputList[i].Equals(inputList[i - 1]))
			{
				outputList.Add(inputList[i]);
			}
		}
		return outputList;
	}
	
	public static LinkedList<T> LinkedListFromList<T>(List<T> inputList)
	{ 
		LinkedList<T> outputList = new LinkedList<T>();

		foreach (T item in inputList)
		{
			outputList.AddLast(item);
		}
		return outputList;
	}

	public static HashSet<T> AnyStudentsElective<T>(Dictionary<string, HashSet<T>> studentElectives)
	{
		HashSet<T> result = new HashSet<T>();

        foreach (var elective in studentElectives.Values)
        {
			result.UnionWith(elective);
        }
		return result;
    }
	public static HashSet<T> AllStudentsElective<T>(Dictionary<string, HashSet<T>> studentsElective)
	{
		HashSet<T> result = new HashSet<T>(studentsElective.Values.First());
		
		foreach (var elective in studentsElective.Values)
		{
			result.IntersectWith(elective);
		}

		return result;
	}
	public static HashSet<T> NoStudentsElective<T>(HashSet<T> electives, Dictionary<string, HashSet<T>> studentsElectives)
	{		
		HashSet<T> anyStudentsElectives = AnyStudentsElective(studentsElectives);
		electives.ExceptWith(anyStudentsElectives);
		return electives;
	}

	private static List<string> Split(string input)
	{
		var result = new List<string>();
		string word = "";
		for (int i = 0; i < input.Length; i++)
		{
			if ((input[i] <= 1103) && (input[i] >= 1040))
			{
				word += input[i];
			}
			else
			{
				result.Add(word);
				word = "";
			}
		}
		result.Add(word);

		return new List<string>(result.Where(word => !string.IsNullOrEmpty(word)));
	}

    private static List<string> SplitSpace(string input)
    {
        var result = new List<string>();
        string word = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != ' ')
            {
                word += input[i];
            }
            else
            {
                result.Add(word);
                word = "";
            }
        }
        result.Add(word);

        return new List<string>(result.Where(word => !string.IsNullOrEmpty(word)));
    }

    public static void LettersInWordsWithNotEvenNumbers(string path)
	{
		try
		{
			using (StreamReader file = new StreamReader(path))
			{
				HashSet<char> letters = new HashSet<char>();
				string text = file.ReadToEnd();
				if (string.IsNullOrEmpty(text))
					return;

				List<string> words = Split(text);
				for (int i = 0; i < words.Count; i += 2)
				{
					for (int j = 0; j < words[i].Length; j++)
						letters.Add(words[i].ToLower()[j]);
				}

				var list = new List<char>(letters);
				list.Sort();
				Console.WriteLine("В нечетных словах встречаются следующие символы: " + string.Join(", ", list));
			}
		}
		catch (IOException e)
		{
			Console.WriteLine("Не удалось открыть файл: " + e.Message);
		}
	}

	public static int WriteToFile(string path)
	{
		try
		{
			using (FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
				List<string> companies = new List<string> {"Компания1 Пушкина 92 1000", "Компания1 Пушкина 98 3000", "Компания2 Ленина 92 2222", "Компания3 Мира 92 1111", "Компания3 Мира 98 2000",
					"Компания4 Куйбышева 92 1000", "Компания4 Куйбышева 98 2000", "Компания5 Коломенская 92 1001", "Компания5 Коломенская 98 2000", "Компания5 Пушкина 98 2000" };
				int n = companies.Count;
				xmlSerializer.Serialize(file, companies);
				return n;
			}
		}
		catch (IOException e)
		{
            Console.WriteLine("Не удалось открыть файл: " + e.Message);
        }
		return 0;
	}

	public static void GasStationsLessExpensiveGasoline(string path, int n)
	{
		try
		{
			if (n == 0)
			{
				Console.WriteLine("0 0 0");
				return;
			}

			using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
				List<string> companies = (List<string>)xmlSerializer.Deserialize(file);

				int lessExpensive92 = int.MaxValue, lessExpensive95 = int.MaxValue, lessExpensive98 = int.MaxValue;
				int num92 = 0, num95 = 0, num98 = 0;

				int index = 0;
				Dictionary<int, string> companiesDict = new Dictionary<int, string>();
				foreach (var copmany in companies)
				{
					companiesDict.Add(index, copmany);
					index++;
				}
				
				for (int i = 0; i < companiesDict.Count; i++)
				{
					List<string> info = SplitSpace(companiesDict[i]);
					switch (info[2])
					{
                        case "92": if (lessExpensive92 > int.Parse(info[3]))
							{
								lessExpensive92 = int.Parse(info[3]);
								num92 = 1;
							}
							else if (lessExpensive92 == int.Parse(info[3]))
								num92++;
						break;
						case "95": if (lessExpensive95 > int.Parse(info[3]))
							{
								lessExpensive95 = int.Parse(info[3]);
								num95 = 1;
							}
							else if (lessExpensive95 == int.Parse(info[3]))
								num95++;
						break;
						case "98": if (lessExpensive98 > int.Parse(info[3]))
							{
								lessExpensive98 = int.Parse(info[3]);
								num98 = 1;
							}
							else if (lessExpensive98 == int.Parse(info[3]))
								num98++;
						break;
                    }
				}

				Console.WriteLine("{0} {1} {2}", num92, num95, num98);
            }
		}
        catch (IOException e)
        {
            Console.WriteLine("Не удалось открыть файл: " + e.Message);
        }
    }
}