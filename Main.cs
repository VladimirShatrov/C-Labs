using System;
using static Matrix;
using static EnterNum;
using static DisplayMatrix;
using static Files;

public class Run
{
    public static void Main(string[] args)
    {
        //1
        Console.WriteLine("Ввод размерности матрицы:");
        int n = enterNum(1), m = enterNum(1);
        Matrix matrix = new Matrix(n, m);
        displayMatrix(matrix, n, m);
        
        Console.WriteLine("Ввод размерности матрицы:");
        n = enterNum(1);
        Matrix matrix2 = new Matrix(n);
        displayMatrix(matrix2, n, n);
        
        Console.WriteLine("Ввод размерности матрицы:");
        n = enterNum(1);
        Matrix matrix3 = new Matrix(n, true);
        displayMatrix(matrix3, n, n);
        //2
        Console.WriteLine("Количество элементов больших любого элемента главной диагонали для первой матрицы: {0}", matrix.moreThenDiagonale());
        //3
        Console.WriteLine(matrix2.ToString());

        Matrix A = new Matrix();
        Matrix B = new Matrix();
        Matrix C = new Matrix();
        int[,] m1 = { { 1,2,3 },{ 6,7,8 },{ -1,-1,-1 } };
        A.Arr = m1;
        int[,] m2 = { { 2,3,8 },{ 0,-1,-3 },{ 1,1,1 } };
        C.Arr = m2;
        int[,] m3 = { { 1,9,8 }, { 1, 9, 8 }, { 1, 9, 8 } };
        B.Arr = m3;
        Console.WriteLine((A * T(C)) + (B * C));

        //4
        BinaryFileWriteNumbers("1.dat", "check.txt");
        Console.WriteLine("Количесвто удвоенных нечетных чисел в этом файле = {0}",BinaryFileNotEvenMultiplyBy2("1.dat"));

        //5
        BinaryFileWriteToys("toys.dat");
        Console.WriteLine("Название саммой дешевой игрушки - {0}", LessExpensiveToy("toys.dat"));

        //6
        WriteNumbersToFile("new.txt");
        Console.WriteLine("Квадрат разности максимального и минимального = {0}", SquareOfDifferenceOfMaxAndMin("new.txt"));

        //7
        WriteNumbersToFile("superNew.txt");
        Console.WriteLine("Сумма нечетных элеметнов = {0}", SumOfNotEvenElemetns("superNew.txt"));

        //8
        NewFileWithLastSymbolFromEvryLineFromFile("old.txt", "taa.txt");
    }
}