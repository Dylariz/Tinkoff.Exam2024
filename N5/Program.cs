using System;
using System.Collections.Generic;

namespace N5;

internal class Program
{
    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        
        // Build the graph
        var tiles = new Tile[n, 3];
        for (int i = 0; i < n; i++)
        {
            char[] line = Console.ReadLine().ToCharArray();
            for (int j = 0; j < 3; j++)
            {
                tiles[i, j] = new Tile(line[j] switch
                {
                    'W' => -1,
                    '.' => 0,
                    'C' => 1,
                    _ => throw new ArgumentException()
                });

                if (i > 0 && tiles[i, j].Value != -1)
                {
                    if (tiles[i - 1, j].Value != -1)
                        tiles[i - 1, j].AddNextTile(tiles[i, j]);

                    if (j > 0 && tiles[i - 1, j - 1].Value != -1)
                        tiles[i - 1, j - 1].AddNextTile(tiles[i, j]);
                    
                    if (j < 2 && tiles[i - 1, j + 1].Value != -1)
                        tiles[i - 1, j + 1].AddNextTile(tiles[i, j]);
                    
                }
            }
        }
        
        // Find all paths and their profit 
        int maxProfit = 0;
        for (int i = 0; i < 3; i++)
        {
            if (tiles[0, i].Value != -1)
            {
                var stack = new Stack<(Tile, int)>();
                stack.Push((tiles[0, i], tiles[0, i].Value));
                
                while (stack.Count > 0)
                {
                    var (currentTile, profit) = stack.Pop();
                    if (currentTile.NextTiles.Count == 0)
                    {
                        maxProfit = Math.Max(maxProfit, profit);
                    }
                    else
                    {
                        foreach (var nextTile in currentTile.NextTiles)
                        {
                            stack.Push((nextTile, profit + nextTile.Value));
                        }
                    }
                }
            }
        }
        
        Console.WriteLine(maxProfit);
    }
}
    
public class Tile
{
    public int Value { get; } // -1 = W; 0 = .; 1 = C;
    
    public IReadOnlyList<Tile> NextTiles => _nextTiles;

    private List<Tile> _nextTiles = new();
    
    public Tile(int value)
    {
        Value = value;
    }
    
    public void AddNextTile(Tile neighbour)
    {
        _nextTiles.Add(neighbour);
    }
}