namespace AdventOfCode2023.Days;

public class Day03(string inputFileName) : AocChallenge(inputFileName)
{
    public override void Part01()
    {
        var lines = ChallengeFileString.Split("\n");
        
        // this could be done in 1 loop instead of 2, i know
        var numbers = ParseNumbers(lines);
        var symbols = FindSymbols(lines);

        var partNumberSum = 0;

        foreach (var partNumber in numbers)
        {
            partNumberSum += (
                from symbol in symbols
                where symbol.Column >= partNumber.Begin - 1
                where symbol.Column <= partNumber.End + 1
                where Math.Abs(partNumber.Line - symbol.Line) <= 1
                select int.Parse(partNumber.Chars)).Sum();
        }
        
        Console.WriteLine($"[DAY03][PT1] Sum of part numbers: {partNumberSum}");
    }

    public override void Part02()
    {
        throw new NotImplementedException();
    }

    private static List<Symbol> FindSymbols(IReadOnlyList<string> lines)
    {
        var symbols = new List<Symbol>();

        for (var iLine = 0; iLine < lines.Count; iLine++)
        {
            var line = lines[iLine];
            for (var iChar = 0; iChar < line.Length; iChar++)
            {
                var c = line[iChar];

                if (c is < '0' or > '9' && c != '.')
                {
                    symbols.Add(new Symbol(c, iLine, iChar));
                }
            }
        }

        return symbols;
    }

    private static List<Number> ParseNumbers(IReadOnlyList<string> lines)
    {
        var numbers = new List<Number>();
        
        for(var line = 0; line < lines.Count; line++)
        {
            var currentNum = "";
            var currentNumBegin = 0;

            var chars = lines[line].ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                if (chars[i] is >= '0' and <= '9')
                {
                    if(currentNum.Length == 0)
                        currentNumBegin = i;

                    currentNum += chars[i];
                }
                else if(currentNum.Length > 0)
                {
                    numbers.Add(new Number(currentNum, line, currentNumBegin, i-1));
                    currentNum = "";
                    currentNumBegin = 0;
                }
            }
        }

        return numbers;
    }
}

internal class Number(string chars, int line, int begin, int end)
{
    public string Chars { get; set; } = chars;
    public int Line { get; set; } = line;
    public int Begin { get; set; } = begin;
    public int End { get; set; } = end;
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