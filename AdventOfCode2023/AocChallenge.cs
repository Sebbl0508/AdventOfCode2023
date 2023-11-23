namespace AdventOfCode2023;

public abstract class AocChallenge
{
    private string challangeFilePath;
    
    public AocChallenge()
    {
        challangeFilePath = "PLACEHOLDER";
    }
    
    public byte[] LoadChallengeFileBytes()
    {
        throw new NotImplementedException();
    }

    public string LoadChallengeFileString()
    {
        throw new NotImplementedException();
    }
    
    public abstract void Part01();
    public abstract void Part02();
}