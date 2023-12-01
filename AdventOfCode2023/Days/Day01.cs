namespace AdventOfCode2023.Days;

public class Day01(string inputFileName) : AocChallenge(inputFileName)
{
    static readonly Dictionary<string, uint> WordNumbers = new()
    {
        ["one"] = 1,
        ["two"] = 2,
        ["three"] = 3,
        ["four"] = 4,
        ["five"] = 5,
        ["six"] = 6,
        ["seven"] = 7,
        ["eight"] = 8,
        ["nine"] = 9
    };

    public override void Part01()
    {
        var lines = ChallengeFileString.Split("\n");
        uint sumOfCalibrationValues = 0;

        foreach (var line in lines)
        {
            var numbers = line.Where(c => c is >= '0' and <= '9').ToList();
            uint calibrationValue = uint.Parse($"{numbers[0]}{numbers[^1]}");

            sumOfCalibrationValues += calibrationValue;
        }

        Console.WriteLine($"[DAY01][PT1] Sum of calibration values: {sumOfCalibrationValues}");
    }

    public override void Part02()
    {
        var lines = ChallengeFileString.Split("\n");
        uint sumOfCalibrationValues = 0;

        foreach (var line in lines)
        {
            var foundNumbers = new List<SubstringWithIndex>();

            // search number characters first
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] is >= '0' and <= '9')
                    foundNumbers.Add(new(i, line[i].ToString()));
            }

            // find all occurrences of number words
            foreach (var numWord in WordNumbers)
            {
                var a = line.AllIndicesOf(numWord.Key);
                foundNumbers = foundNumbers.Concat(a.Select(index => new SubstringWithIndex(index, numWord.Key))).ToList();
            }

            // sort found numbers (words or chars) by their order in the line
            foundNumbers.Sort((index1, index2) => index1.Index.CompareTo(index2.Index));

            var numbers = foundNumbers.Select<SubstringWithIndex, uint>(x => WordNumbers.ContainsKey(x.SubString) ? WordNumbers.GetValueOrDefault(x.SubString) : uint.Parse(x.SubString)).ToList();

            uint calibrationValue = uint.Parse($"{numbers[0]}{numbers[^1]}");
            sumOfCalibrationValues += calibrationValue;
        }

        Console.WriteLine($"[DAY01][PT2] Sum of calibration values: {sumOfCalibrationValues}");
    }
}

struct SubstringWithIndex
{
    public SubstringWithIndex(int index, string subString)
    {
        Index = index;
        SubString = subString;
    }

    public readonly int Index;
    public readonly string SubString;
}