using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days;

public class Day05(string inputFileName) : AocChallenge(inputFileName)
{
    public override void Part01()
    {
        var lines = Regex.Split(ChallengeFileString, "\r\n|\r|\n");

        var almanac = new Almanac(lines);
        almanac.DebugPrint();
    }

    public override void Part02()
    {
        throw new NotImplementedException();
    }
}

class Almanac
{
    public Int64[] Seeds { get; set; }
    public List<Map> Maps { get; set; }
    
    
    public Almanac(string[] lines)
    {
        Seeds = lines[0]
                    .Split(": ")[1]
                    .Split(" ")
                    .Select(Int64.Parse)
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
    
    public List<Range> Ranges { get; set; }
}

class Range
{
    public Range(Int64 srcRangeStart, Int64 dstRangeStart, Int64 rangeLength)
    {
        SrcRangeStart = srcRangeStart;
        DstRangeStart = dstRangeStart;
        RangeLength = rangeLength;
    }

    public Int64 SrcRangeStart { get; set; }
    public Int64 DstRangeStart { get; set; }
    public Int64 RangeLength { get; set; }
}