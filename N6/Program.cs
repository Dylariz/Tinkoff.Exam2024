using System;
using System.Collections.Generic;

namespace N6;

internal class Program
{
    // Pairs of all possible moves for each direction, first 8 for Knight, last 8 for King
    private static readonly int[] Dx = { -2, -1, 1, 2, 2, 1, -1, -2, -1, 0, 1, 0, -1, 1, 1, -1, -1, 1 };
    private static readonly int[] Dy = { 1, 2, 2, 1, -1, -2, -2, -1, -1, -1, -1, 1, 1, 1, 0, 0, 1, -1 };

    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        char[,] board = new char[n, n];
        int[,,] dist = new int[n, n, 2]; // dist[x, y, type]
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                dist[i, j, 0] = dist[i, j, 1] = int.MaxValue;
            }
        }

        var start = (0, 0);
        var end = (0, 0);

        for (int i = 0; i < n; i++)
        {
            string line = Console.ReadLine();
            for (int j = 0; j < n; j++)
            {
                board[i, j] = line[j];
                if (board[i, j] == 'S')
                {
                    start = (i, j);
                }
                else if (board[i, j] == 'F')
                {
                    end = (i, j);
                }
            }
        }

        Queue<(int, int, int)> queue = new Queue<(int, int, int)>(); // (x, y, type)
        queue.Enqueue((start.Item1, start.Item2, 0));
        dist[start.Item1, start.Item2, 0] = 0;

        while (queue.Count > 0)
        {
            var (x, y, type) = queue.Dequeue(); // type = 0 for Knight, 1 for King
            for (int i = type * 8; i < (type + 1) * 8; i++)
            {
                // Coordinates of the possible move
                int nx = x + Dx[i];
                int ny = y + Dy[i];
                
                if (nx >= 0 && nx < n && ny >= 0 && ny < n) // Check if the move is inside the board
                {
                    int nt = board[nx, ny] == 'K' ? 0 : board[nx, ny] == 'G' ? 1 : type;
                    
                    // Check if the new distance to the cell is less than the previous one
                    if (dist[nx, ny, nt] > dist[x, y, type] + 1) 
                    {
                        dist[nx, ny, nt] = dist[x, y, type] + 1; 
                        queue.Enqueue((nx, ny, nt));
                    }
                }
            }
        }

        // Print the minimum distance to the end cell
        int answer = Math.Min(dist[end.Item1, end.Item2, 0], dist[end.Item1, end.Item2, 1]);
        Console.WriteLine(answer == int.MaxValue ? -1 : answer);
    }
}