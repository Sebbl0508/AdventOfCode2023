namespace AdventOfCode2023.Days;

public class Day02(string inputFileName) : AocChallenge(inputFileName)
{
    private static readonly Dictionary<string, int> MaxCubesPart01 = new()
    {
        ["red"] = 12,
        ["green"] = 13,
        ["blue"] = 14
    };

    public override void Part01()
    {
        var lines = ChallengeFileString.Split("\n");
        List<string> possibleIds = new();

        foreach (var gameLine in lines)
        {
            var gameId = gameLine.Split(":")[0].Split(" ")[^1];
            var gameRolls = gameLine.Split(":")[1].Split(";").Select(v => v.Trim().Split(",")).ToArray();
            var gameImpossible = false;

            foreach (var roll in gameRolls)
            {
                if (gameImpossible)
                    break;

                foreach (var cubeInfo in roll)
                {
                    var colorAndCount = cubeInfo.Trim().Split(" ");
                    var cubeCount = int.Parse(colorAndCount[0]);
                    var cubeColor = colorAndCount[1];

                    var maxCubes = MaxCubesPart01.GetValueOrDefault(cubeColor);
                    if (cubeCount <= maxCubes) continue;

                    gameImpossible = true;
                    break;
                }
            }
            if (!gameImpossible)
                possibleIds.Add(gameId);
        }

        var result = possibleIds.Sum(int.Parse);
        Console.WriteLine($"[DAY02][PT1] Sum of all invalid game IDs: {result}");
    }

    public override void Part02()
    {
        var lines = ChallengeFileString.Split("\n");

        var sumOfPowerOfSets = 0;

        foreach (var gameLine in lines)
        {
            var gameId = gameLine.Split(":")[0].Split(" ")[^1];
            var gameRolls = gameLine.Split(":")[1].Split(";").Select(v => v.Trim().Split(",")).ToArray();

            Dictionary<string, int> minPossibleGame = new();

            foreach (var roll in gameRolls)
            {
                foreach (var cubeInfo in roll)
                {
                    var colorAndCount = cubeInfo.Trim().Split(" ");
                    var cubeCount = int.Parse(colorAndCount[0]);
                    var cubeColor = colorAndCount[1];

                    var minCount = minPossibleGame.GetValueOrDefault(cubeColor);
                    if (cubeCount > minCount)
                        minPossibleGame[cubeColor] = cubeCount;
                }
            }

            var powerSetOfCubes = minPossibleGame["red"] * minPossibleGame["green"] * minPossibleGame["blue"];
            sumOfPowerOfSets += powerSetOfCubes;
        }

        Console.WriteLine($"[DAY02][PT2] Sum of the power of the sets: {sumOfPowerOfSets}");
    }
}