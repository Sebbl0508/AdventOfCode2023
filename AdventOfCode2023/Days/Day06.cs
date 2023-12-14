using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days;

public class Day06(string inputFileName) : AocChallenge(inputFileName)
{
    public override void Part01()
    {
        var races = ParseRaces();

        var numWaysToWin = races.Select(Boat.GetNumWaysToWin).ToList();

        var sumNumWaysToWin = numWaysToWin.Aggregate((long)1, (v, x) => v * x);

        Console.WriteLine($"[DAY06][PT1] Multiplied number of ways to win: {sumNumWaysToWin}");
    }

    public override void Part02()
    {
        var race = ParseRace();

        var numWaysToWin = Boat.GetNumWaysToWin(race);

        Console.WriteLine($"[DAY06][PT2] Number of ways to win: {numWaysToWin}");
    }

    Race[] ParseRaces()
    {
        var lines = Regex.Split(ChallengeFileString, "\r\n|\r|\n");

        var times = lines[0]
            .Split(":")[1]
            .Split(" ")
            .Where(v => v != "" && v != " ")
            .Select(long.Parse)
            .ToArray();

        var distances = lines[1]
            .Split(":")[1]
            .Split(" ")
            .Where(v => v != "" && v != " ")
            .Select(long.Parse)
            .ToArray();

        return times.Zip(distances).Select(v => new Race(v.First, v.Second)).ToArray();
    }

    Race ParseRace()
    {
        var lines = Regex.Split(ChallengeFileString, "\r\n|\r|\n");

        var time = long.Parse(string.Join("", lines[0]
            .Split(":")[1]
            .Split(" ")
            .Where(v => v != "" && v != " ")));

        var distance = long.Parse(string.Join("", lines[1]
            .Split(":")[1]
            .Split(" ")
            .Where(v => v != "" && v != " ")));

        return new Race(time, distance);
    }
}

static class Boat
{
    private const long StartingSpeed = 0;
    /// <summary>
    /// For each millisecond the button is held, the boat will accelerate one mm per ms
    ///
    /// Example: Button is held down 3ms => speed = 3mm/ms => after 2ms the boat travelled 6mm
    /// </summary>
    private const long SpeedIncreasePerMs = 1;

    public static long GetNumWaysToWin(Race race)
    {
        long winPossibilities = 0;

        for (var buttonHoldTime = 0; buttonHoldTime < race.AvlTime; buttonHoldTime++)
        {
            var timeLeft = race.AvlTime - buttonHoldTime;
            var travelDistance = timeLeft * buttonHoldTime;

            if (travelDistance > race.RecordDistance)
            {
                winPossibilities += 1;
            }
        }

        return winPossibilities;
    }
}

class Race
{
    public Race(long avlTime, long recordDistance)
    {
        AvlTime = avlTime;
        RecordDistance = recordDistance;
    }

    /// <summary>
    /// Available time in milliseconds
    /// </summary>
    public long AvlTime { get; set; }

    /// <summary>
    /// Record distance in millimeters
    /// </summary>
    public long RecordDistance { get; set; }
}