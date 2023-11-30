namespace AdventOfCode2023;

public abstract class AocChallenge
{
    protected readonly string ChallengeFileString;
    protected readonly byte[] ChallengeFileBytes;

    public AocChallenge(string inputFileName)
    {
        var challengeFilePath = Path.Combine(".", "Inputs", inputFileName);
        ChallengeFileString = File.ReadAllText(challengeFilePath);
        ChallengeFileBytes = File.ReadAllBytes(challengeFilePath);
    }

    public abstract void Part01();
    public abstract void Part02();
}