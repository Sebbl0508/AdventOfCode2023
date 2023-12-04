﻿using AdventOfCode2023.Days;

namespace AdventOfCode2023;

internal class Program
{
    private static uint AdventDays = 25;
    private static bool UseExampleInput = false;

    static void Main(string[] args)
    {
        var day04 = new Day04("Day04-example.txt");
        day04.Part02();

        //RunDaysSequential(UseExampleInput);
    }

    static void RunDaysSequential(bool useExampleInput)
    {
        for (uint day = 1; day <= AdventDays; day++)
        {
            var inputName = $"Day{day:00}.txt";
            if (useExampleInput)
                inputName = $"Day{day:00}-example.txt";

            var typeName = $"AdventOfCode2023.Days.Day{day:00}";
            var dayT = Type.GetType(typeName);

            if (dayT == null || Activator.CreateInstance(dayT, inputName) is not AocChallenge dayObj)
            {
                Console.WriteLine($"[*] Day {day:00} can't be run");
                continue;
            }

            try
            {
                dayObj.Part01();
                dayObj.Part02();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[*] Caught exception while running day {day:00}:\n{e}");
            }
        }
    }
}