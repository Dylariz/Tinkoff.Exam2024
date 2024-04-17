using System;
using System.Collections.Generic;
using System.Linq;

namespace N3;

internal class Program
{
    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());

        Dir root = null;
            
        for (int i = 0; i < n; i++)
        {
            string[] path = Console.ReadLine().Split('/');
                
            if (root == null)
                root = new Dir(path[0]);
                
            Dir current = root;
                
            for (int j = 1; j < path.Length; j++)
            {
                Dir dir = new Dir(path[j]);
                current = current.TryToAddDir(dir);
            }
        }
            
        root.PrintAll();
    }
}
    
public class Dir
{
    public string Name { get; }
        
    public IReadOnlyList<Dir> Directories => _directories;
        
    private List<Dir> _directories;
        
    public Dir(string name)
    {
        Name = name;
        _directories = new List<Dir>();
    }
        
    public Dir TryToAddDir(Dir newDir)
    {
        // Find if the directory already exists
        var dir = _directories.FirstOrDefault(x => x.Name == newDir.Name);

        if (dir == null)
        {
            _directories.Add(newDir);
            return newDir;
        }

        return dir;
    }
        
    public void PrintAll(int depth = 0)
    {
        Console.WriteLine($"{new string(' ', depth * 2)}{Name}");
        foreach (var dir in _directories)
        {
            dir.PrintAll(depth + 1);
        }
    }
}