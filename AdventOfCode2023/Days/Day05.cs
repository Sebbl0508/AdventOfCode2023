using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days;

public class Day05(string inputFileName) : AocChallenge(inputFileName)
{
    public override void Part01()
    {
        var lines = Regex.Split(ChallengeFileString, "\r\n|\r|\n");

        var almanac = new Almanac(lines);

        var lowest = almanac.Seeds
            .Select(seed => almanac.Maps.Aggregate(seed, (current, map) => map.Convert(current)))
            .Prepend(long.MaxValue)
            .Min();

        Console.WriteLine($"[DAY05][PT1] Lowest location: {lowest}");
    }

    public override void Part02()
    {
        Console.WriteLine("[DAY05][PT2] Skipping (naive solution; take too long!)");
        return;

        var lines = Regex.Split(ChallengeFileString, "\r\n|\r|\n");

        var almanac = new Almanac(lines);

        var lowest = long.MaxValue;

        foreach (var seed in almanac.SeedRanges.GetSeedNumbers())
        {
            var num = seed;
            foreach (var map in almanac.Maps)
            {
                num = map.Convert(num);
            }

            if (num < lowest) lowest = num;
        }

        Console.WriteLine($"[DAY05][PT2] Lowest location: {lowest}");
    }
}

class Almanac
{
    public long[] Seeds { get; set; }

    public SeedRange[] SeedRanges { get; set; }

    public List<Map> Maps { get; set; }


    public Almanac(string[] lines)
    {
        Seeds = lines[0]
            .Split(": ")[1]
            .Split(" ")
            .Select(Int64.Parse)
            .ToArray();

        SeedRanges = lines[0]
            .Split(": ")[1]
            .Split(" ")
            .Chunk(2)
            .Select(v => new SeedRange(Int64.Parse(v[0]), Int64.Parse(v[1])))
            .ToArray();

        Maps = new List<Map>();

        Map? currentMap = null;
        foreach (var line in lines.Skip(2))
        {
            if (line.Contains("-to-"))
            {
                // Begin of map
                var map = line.Split(" ")[0];

                var mapNameParts = map.Split("-");

                currentMap = new Map(mapNameParts[0], mapNameParts[2], new List<Range>());
            }
            else if (line == string.Empty)
            {
                // End of map
                if (currentMap != null)
                    Maps.Add(currentMap);
                else
                    throw new UnreachableException();
            }
            else
            {
                var numbers = line.Split(" ").Select(Int64.Parse).ToArray();
                currentMap?.Ranges.Add(new Range(numbers[1], numbers[0], numbers[2]));
            }
        }
        if (currentMap != null)
            Maps.Add(currentMap);
        else
            throw new UnreachableException();
    }

    public void DebugPrint()
    {
        Console.WriteLine($"Almanac has {Maps.Count} maps and {Seeds.Length} seeds need to be planted:");

        Console.WriteLine($"  Seeds: {string.Join(" ", Seeds)}");

        Console.WriteLine($"  Seed ranges: {string.Join(" | ", SeedRanges.Select(v => v.Debug))}");

        foreach (var map in Maps)
        {
            Console.WriteLine($"  Map {map.From}-{map.To} has {map.Ranges.Count} ranges:");

            foreach (var range in map.Ranges)
            {
                Console.WriteLine($"    {range.DstRangeStart} {range.SrcRangeStart} {range.RangeLength}");
            }
        }
    }
}

class Map
{
    public Map(string from, string to, List<Range> ranges)
    {
        From = from;
        To = to;
        Ranges = ranges;
    }

    public string From { get; set; }
    public string To { get; set; }

    public long Convert(long num)
    {
        foreach (var range in Ranges)
        {
            var res = range.Convert(num);
            if (res != num) return res;
        }

        return num;
    }

    public List<Range> Ranges { get; set; }
}

class Range
{
    public Range(long srcRangeStart, long dstRangeStart, long rangeLength)
    {
        SrcRangeStart = srcRangeStart;
        DstRangeStart = dstRangeStart;
        RangeLength = rangeLength;
    }

    /// <summary>
    /// Converts the input number from source range to destination range
    /// </summary>
    public long Convert(long num)
    {
        if (num >= SrcRangeStart && num < SrcRangeStart + RangeLength)
        {
            return DstRangeStart + (num - SrcRangeStart);
        }

        return num;
    }

    public long SrcRangeStart { get; set; }
    public long DstRangeStart { get; set; }
    public long RangeLength { get; set; }
}

class SeedRange
{
    public SeedRange(long start, long length)
    {
        Start = start;
        Length = length;
    }

    public bool IsInRange(long num)
    {
        return num >= Start && num < Start + Length;
    }

    public string Debug => $"{Start}=>{Start + Length - 1} ({Length})";
    public long End => Start + Length;

    public long Start { get; set; }
    public long Length { get; set; }
}

static class SeedRangesExtensions
{
    public static IEnumerable<long> GetSeedNumbers(this IEnumerable<SeedRange> seedRanges)
    {
        foreach (var seedRange in seedRanges)
        {
            for (var seed = seedRange.Start; seed < seedRange.End; seed++)
            {
                yield return seed;
            }
        }
    }
}