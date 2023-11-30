using AdventOfCode2023.Days;

namespace AdventOfCode2023;

internal class Program
{
    private static uint AdventDays = 25;

    static void Main(string[] args)
    {
        Console.WriteLine("Hello World");

        Example exampleDay = new();
        exampleDay.Part01();
        exampleDay.Part02();

        RunDaysSequential();
    }

    static void RunDaysSequential()
    {
        for (uint day = 1; day <= AdventDays; day++)
        {
            var inputName = $"Day{day:00}.txt";
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
            }
            catch (Exception e)
            {
                Console.WriteLine($"[*] Caught exception while running day {day:00}:\n{e}");
            }
        }
    }
}