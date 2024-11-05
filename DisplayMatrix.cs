using System;

public class DisplayMatrix
{
    public static void displayMatrix(Matrix matrix, int n, int m)
    {
        int[,] array = matrix.Arr;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write(array[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}