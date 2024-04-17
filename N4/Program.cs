using System;
using System.Collections.Generic;

namespace N4;

internal class Program
{
    static void Main()
    {
        string[] input = Console.ReadLine().Split();
        int n = int.Parse(input[0]);
        bool direction = input[1] == "R";

        int[,] matrix = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            string[] line = Console.ReadLine().Split();
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = int.Parse(line[j]);
            }
        }
        
        int[,] rotatedMatrix = RotateMatrix(matrix, direction);
        
        // Find all operations
        var operations = new List<(int, int, int, int)>();
        
        var used = new bool[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++) // matrix
            {
                if (rotatedMatrix[i, j] != matrix[i, j])
                {
                    for (int k = 0; k < n; k++)
                    {
                        for (int l = 0; l < n; l++) // rotatedMatrix
                        {
                            if (rotatedMatrix[i, j] == matrix[k, l] && !used[k, l])
                            {
                                used[k, l] = true;
                                if (rotatedMatrix[k, l] == matrix[k, l])
                                {
                                    continue;
                                }
                                
                                (matrix[i, j], matrix[k, l]) = (matrix[k, l], matrix[i, j]);
                                operations.Add((i, j, k, l));

                                l = n;
                                k = n;
                            }
                        }

                    }
                }
            }
        }
        
        Console.WriteLine(operations.Count);
        foreach (var operation in operations)
        {
            Console.WriteLine($"{operation.Item1} {operation.Item2} {operation.Item3} {operation.Item4}");
        }
    }
    
    public static int[,] RotateMatrix(int[,] matrix, bool direction) // true - Right, false - Left
    {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);
        
        int[,] rotatedMatrix = new int[m, n];

        if (direction)
        {
            for (int j = 0; j < m; j++)
            {
                for (int i = n-1; i >= 0; i--)
                {
                    rotatedMatrix[j, n-i-1] = matrix[i, j];
                }
            }
        }
        else
        {
            for (int j = m - 1; j >= 0; j--)
            {
                for (int i = 0; i < n; i++)
                {
                    rotatedMatrix[m-j-1, i] = matrix[i, j];
                }
            }
        }
        
        
        return rotatedMatrix;
    }
}