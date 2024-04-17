using System;
using System.Linq;

namespace N1;

internal static class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] grades = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int start = 0, end = 6, currentFives = -1;

        // Find the first 7 grades
        while (currentFives == -1)
        {
            currentFives = CountFives(grades, start, end, out int index);

            if (currentFives == -1)
            {
                start = index + 1;
                end = index + 7;
            }

            if (end >= n)
            {
                Console.WriteLine(-1);
                return;
            }
        }

        // Find the maximum number of fives
        int maxFives = currentFives;

        while (end < n)
        {
            start++;
            end++;

            currentFives = CountFives(grades, start, end, out int index);
            if (currentFives != -1)
            {
                maxFives = Math.Max(maxFives, currentFives);
            }
            else
            {
                start = index + 1;
                end = index + 7;
            }
        }

        Console.WriteLine(maxFives);
    }

    // Count the number of fives in the given range, -1 if there is a 2 or 3
    static int CountFives(int[] grades, int start, int end, out int index)
    {
        int fives = 0;

        for (int i = start; i < end; i++)
        {
            if (grades[i] == 5)
            {
                fives++;
            }
            else if (grades[i] == 2 || grades[i] == 3)
            {
                index = i;
                return -1;
            }
        }

        index = -1;
        return fives;
    }
}