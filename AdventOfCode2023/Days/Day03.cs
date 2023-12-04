using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days;

public class Day03(string inputFileName) : AocChallenge(inputFileName)
{
    public override void Part01()
    {
        var lines = Regex.Split(ChallengeFileString, "\r\n|\r|\n");

        var symbols = FindSymbols(lines);
        var partNumbers = FindPartNumbers(lines);

        var validPartNumbers =
            from partNumber in partNumbers
            where symbols.Any(sym => IsAdjacent(sym, partNumber))
            select int.Parse(partNumber.Chars);

        Console.WriteLine($"[DAY03][PT1] Sum of valid part numbers: {validPartNumbers.Sum()}");
    }

    public override void Part02()
    {
        var lines = Regex.Split(ChallengeFileString, "\r\n|\r|\n");

        var symbols = FindSymbols(lines);
        var partNumbers = FindPartNumbers(lines);

        var gearRatios =
            from gear in symbols
            let adjacentPartNumbers =
                from partNumber in partNumbers
                where IsAdjacent(gear, partNumber)
                select int.Parse(partNumber.Chars)
            where adjacentPartNumbers.Count() == 2
            where gear.Character == '*'
            select adjacentPartNumbers.First() * adjacentPartNumbers.Last();

        Console.WriteLine($"[DAY03][PT2] Sum of gear ratios: {gearRatios.Sum()}");
    }

    private Symbol[] FindSymbols(string[] lines)
    {
        var symbols = new List<Symbol>();

        for (var iLine = 0; iLine < lines.Length; iLine++)
        {
            var line = lines[iLine];
            for (var iChar = 0; iChar < line.Length; iChar++)
            {
                var c = line[iChar];
                if (c is < '0' or > '9' && c != '.')
                    symbols.Add(new Symbol(c, iLine, iChar));
            }
        }

        return symbols.ToArray();
    }

    private PartNumber[] FindPartNumbers(string[] lines)
    {
        var partNumbers = new List<PartNumber>();

        for (var iLine = 0; iLine < lines.Length; iLine++)
        {
            var line = lines[iLine];

            var numBuilder = "";
            var numBeginCol = 0;

            for (var iChar = 0; iChar < line.Length; iChar++)
            {
                var c = line[iChar];

                if (c is >= '0' and <= '9')
                {
                    if (numBuilder == "")
                        numBeginCol = iChar;

                    numBuilder += c;
                }
                else if (numBuilder.Length > 0)
                {
                    partNumbers.Add(new PartNumber(numBuilder, iLine, numBeginCol));
                    numBuilder = "";
                    numBeginCol = -1;
                }

                // If we're at the end of the line save the number
                if (iChar >= line.Length - 1)
                {
                    partNumbers.Add(new PartNumber(numBuilder, iLine, numBeginCol));
                    numBuilder = "";
                    numBeginCol = -1;
                }
            }
        }

        return partNumbers.ToArray();
    }

    private static bool IsAdjacent(Symbol p1, PartNumber p2)
    {
        return Math.Abs(p2.Line - p1.Line) <= 1 &&
               p1.Column <= p2.Column + p2.Chars.Length &&
               p2.Column <= p1.Column + 1;
    }
}

internal class PartNumber(string chars, int line, int column)
{
    public string Chars { get; set; } = chars;
    public int Line { get; set; } = line;
    public int Column { get; set; } = column;
}

internal class Symbol
{
    public Symbol(char character, int line, int column)
    {
        Character = character;
        Line = line;
        Column = column;
    }

    public char Character { get; set; }
    public int Line { get; set; }
    public int Column { get; set; }
}