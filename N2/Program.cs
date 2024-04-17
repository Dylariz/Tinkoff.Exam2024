using System;
using System.Linq;

namespace N2;

internal class Program
{
    public static void Main(string[] args)
    {
        // n - Rows, m - Columns
        var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
        (int n, int m) = (input[0], input[1]);
        
        int[,] matrix = new int[n, m];

        for (int i = 0; i < n; i++)
        {
            string[] row = Console.ReadLine().Split(' ');
            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = int.Parse(row[j]);
            }
        }
        
        int[,] rotatedMatrix = RotateMatrix(matrix);
        
        for (int i = 0; i < rotatedMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < rotatedMatrix.GetLength(1); j++)
            {
                Console.Write(rotatedMatrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    
    public static int[,] RotateMatrix(int[,] matrix)
    {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);
        
        int[,] rotatedMatrix = new int[m, n];

        for (int j = 0; j < m; j++)
        {
            for (int i = n-1; i >= 0; i--)
            {
                rotatedMatrix[j, n-i-1] = matrix[i, j];
            }
        }
        
        return rotatedMatrix;
    }
}