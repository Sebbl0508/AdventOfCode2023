namespace AdventOfCode2023.Days;

public class Example : AocChallenge
{
    public Example() : base("Example.txt")
    {
    }

    public override void Part01()
    {
        Console.WriteLine($"[EXAMPLE][PT1] Output:\n{ChallengeFileString}");
    }

    public override void Part02()
    {
        Console.WriteLine($"[EXAMPLE][PT2] Len: {ChallengeFileBytes.Length}");
    }
}