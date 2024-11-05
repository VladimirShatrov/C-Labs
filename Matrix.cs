using System;
using System.Data.Common;
using static EnterNum;

public class Matrix
{
    private int[,] arr;

    public int[,] Arr { 
        get { return arr; }
        set { arr = value; }
    }

    public Matrix(int n, int m)
    {
        int[,] newArr = new int[n,m];
        Console.WriteLine("Ввод элементов массива:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                newArr[i, j] = enterNum();
            }
        }
        this.arr = newArr;
    }

    public Matrix(int n)
    {
        int[,] newArr = new int[n, n];
        Random rnd = new Random();
        int fillElements = 0, fillEvenElemets = 0, fillNotEvenElements = 0;
        int whiteCounter = 0, blackCounter = 1;
        while (fillElements < n * n)
        {
            int num = rnd.Next(1,101);
            if (num % 2 == 0)
            {
                if (fillEvenElemets < n * n / 2 + n % 2)
                {
                    newArr[whiteCounter / n, whiteCounter % n] = num;
                    if (whiteCounter / n < (whiteCounter + 2) / n)// переход на новую строку
                    {
                        if (whiteCounter % n == n - 2)
                            whiteCounter += 2 + 1 - n % 2;
                        else if (whiteCounter % n == n - 1)
                            whiteCounter += 1 + n % 2;
                    }
                    else
                    {
                        whiteCounter += 2;
                    }
                    fillEvenElemets++;
                    fillElements++;
                }
            }
            else
            {
                if (fillNotEvenElements < n * n / 2)
                {
                    newArr[blackCounter / n, blackCounter % n] = num;
                    if (blackCounter / n < (blackCounter + 2) / n)
                    {
                        if (blackCounter % n == n - 2)
                            blackCounter += 2 + 1 - n % 2;
                        else if (blackCounter % n == n - 1)
                            blackCounter += 1 + n % 2;
                    }
                    else
                    { 
                        blackCounter += 2;
                    }
                    fillNotEvenElements++;
                    fillElements++;
                }
            }
        }
        this.arr = newArr;
    }

    public Matrix(int n, bool b)
    {
        int num = 1, i = n - 1, j = n - 1;
        int[,] newArr = new int[n,n];

        while (num <= n * n)
        {
            newArr[i,j] = num;
            num++;
            if (i != 0 && j != n - 1 && newArr[i - 1, j + 1] == 0)
            {
                i--;
                j++;
            }
            else if (i != n - 1 && j != 0 && newArr[i + 1, j - 1] == 0)
            {
                i++;
                j--;
            }
            else if ((i == n - 1 || i == 0) && j != 0)
            {
                j--;
            }
            else
            {
                i--;
            }
        }
        this.arr = newArr;
    }

    public Matrix(int n, int m, bool b)
    {
        this.arr = new int[n,m];
    }

    public Matrix()
    {
        this.arr = new int[0, 0];
    }

    public int moreThenDiagonale()
    {
        int result = 0;
        int minValueOnDiagonale = int.MaxValue;
        int n = arr.GetLength(0), m = arr.GetLength(1);
        int i = 0, j = 0;
        while (i < n &&  j < m)
        {
            if (arr[i,j] < minValueOnDiagonale)
                minValueOnDiagonale = arr[i,j];
            i++;
            j++;
        }
        for (i = 0; i < n; i++)
            for (j = 0; j < m; j++)
            {
                if (i != j)
                {
                    if (arr[i, j] > minValueOnDiagonale)
                        result++;
                }
            }
        return result;
    }

    public override String ToString()
    {
        if (this.arr == null)
            return String.Empty;
        int n = arr.GetLength(0);
        int m = arr.GetLength(1);
        String result = "";
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                result += String.Format("{0} \t", arr[i, j]);
            }
            result += "\n";
        }
        return result;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        int n_a = a.Arr.GetLength(0), n_b = b.Arr.GetLength(0), m_a = a.Arr.GetLength(1), m_b = b.Arr.GetLength(1);
        if (m_a != n_b)
        {
            throw new ArgumentException("Количесвто столбцов первой матрицы должно быть равно количесвту строквторой.");
        }
        Matrix matrix = new Matrix(n_b, m_a, true);

        for (int i = 0; i < n_a; i++)
        {
            for(int j = 0; j < m_b; j++)
            {
                for (int k = 0; k < m_a; k++)
                {
                    matrix.Arr[i, j] += a.Arr[i, k] * b.Arr[k, j];
                }
            }
        }

        return matrix;
    }

    public static Matrix operator +(Matrix a, Matrix b)
    {
        int n_a = a.Arr.GetLength(0), n_b = b.Arr.GetLength(0), m_a = a.Arr.GetLength(1), m_b = b.Arr.GetLength(1);
        if (m_a != m_b && n_a != n_b) { throw new ArgumentException("Размерности матриц должы быть равны."); }

        Matrix matrix = new Matrix(n_a, m_a, true);
        for (int i = 0; i < n_a; i++)
            for (int j = 0; j < m_a; j++)
                matrix.Arr[i, j] = a.Arr[i, j] + b.Arr[i, j];
        return matrix;
    }

    public static Matrix T(Matrix a)
    {
        int n = a.Arr.GetLength(0), m = a.Arr.GetLength(1);
        Matrix matrix = new Matrix (m, n, true);
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                matrix.Arr[i, j] = a.Arr[j, i];
        return matrix;
    }

}
