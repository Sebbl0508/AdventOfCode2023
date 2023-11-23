﻿using AdventOfCode2023.Days;

namespace AdventOfCode2023;

internal class Program
{
    private static uint AdventDays = 25;
    
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World");

        RunDaysSequential();
    }

    static void RunDaysSequential()
    {
        for (uint day = 1; day <= AdventDays; day++)
        {
            var typeName = $"AdventOfCode2023.Days.Day{day:00}";
            var dayT = Type.GetType(typeName);
            if (dayT == null)
            {
                Console.WriteLine($"[*] Day {day:00} can't be run");
                continue;
            }
            
            var dayO = Activator.CreateInstance(dayT) as AocChallenge;

            if (dayO == null)
            {
                Console.WriteLine($"[*] Day {day:00} can't be run");
                continue;
            }

            try
            {
                dayO.Part01();
                dayO.Part02();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[*] Caught exception while running day {day:00}:\n{e}");
            }
        }
    }
}