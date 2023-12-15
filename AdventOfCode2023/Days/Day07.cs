using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days;

public class Day07(string inputFileName) : AocChallenge(inputFileName)
{
    public override void Part01()
    {
        var lines = Regex.Split(ChallengeFileString, "\r\n|\r|\n");
        var game = new Game(lines);

        long winnings = 0;

        for (var i = 0; i < game.Hands.Count; i++)
        {
            winnings += game.Hands[i].Bid * (i + 1);
        }

        Console.WriteLine($"[DAY07][PT1]: Total winnings: {winnings}");
    }

    public override void Part02()
    {
        var lines = Regex.Split(ChallengeFileString, "\r\n|\r|\n");
        var game = new Game(lines, partTwo: true);

        long winnings = 0;

        for (var i = 0; i < game.Hands.Count; i++)
        {
            winnings += game.Hands[i].Bid * (i + 1);
        }

        Console.WriteLine($"[DAY07][PT2]: Total winnings: {winnings}");
    }
}

class Game
{
    public List<Hand> Hands;

    public Game(string[] lines, bool partTwo = false)
    {
        Hands = lines
            .Select(line => new Hand(line, partTwo))
            .ToList();

        Hands.Sort((v, x) => v.CompareTo(x, partTwo));
    }
}

class Hand
{
    public char[] Cards;
    public HandType HandType;
    public int Bid;

    public Hand(string line, bool partTwo = false)
    {
        var parts = line.Split(" ");
        Cards = parts[0].Trim().ToCharArray();
        Bid = int.Parse(parts[1].Trim());
        HandType = CheckType(partTwo);
    }

    public int CompareTo(Hand other, bool partTwo = false)
    {
        var handTypeCmp = HandType.CompareTo(other.HandType);
        if (handTypeCmp == 0)
        {
            foreach (var cards in Cards.Zip(other.Cards))
            {
                var firstCardVal = cards.First.GetCardValue(partTwo);
                var secondCardVal = cards.Second.GetCardValue(partTwo);

                if (firstCardVal == secondCardVal)
                    continue;
                if (firstCardVal > secondCardVal)
                    return 1;
                if (firstCardVal < secondCardVal)
                    return -1;
            }
        }

        return handTypeCmp;
    }

    private HandType CheckType(bool partTwo = false)
    {
        Dictionary<char, int> cardNums = GetCardNums();

        if (partTwo)
        {
            var cardWithHighestSum = cardNums.MaxBy(kv => kv.Value);
            for (var i = 0; i < Cards.Length; i++)
            {
                if (Cards[i] == 'J')
                    Cards[i] = cardWithHighestSum.Key;
            }

            cardNums = GetCardNums();
        }

        var highestNumSimilar = cardNums.MaxBy(kv => kv.Value);

        if (highestNumSimilar.Value == 5)
            return HandType.FiveOfAKind;

        if (highestNumSimilar.Value == 4)
            return HandType.FourOfAKind;

        if (cardNums.Count == 2 && cardNums.First().Value is 2 or 3 && cardNums.Last().Value is 2 or 3)
            return HandType.FullHouse;

        if (cardNums.Count == 3 && cardNums.Any(v => v.Value == 3))
            return HandType.ThreeOfAKind;

        var numPairs = cardNums.Count(kv => kv.Value == 2);

        if (cardNums.Count == 3 && numPairs == 2)
            return HandType.TwoPair;

        if (highestNumSimilar.Value == 2)
            return HandType.OnePair;

        return HandType.HighCard;
    }

    private Dictionary<char, int> GetCardNums()
    {
        Dictionary<char, int> cardNums = new();

        foreach (var card in Cards)
        {
            if (!cardNums.TryGetValue(card, out _))
            {
                cardNums.Add(card, 1);
            }
            else
            {
                cardNums[card] += 1;
            }
        }

        return cardNums;
    }
}

enum HandType
{
    HighCard = 0,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
}

static class CharExtensions
{
    private static readonly Dictionary<char, int> CardValuesPt1 = new Dictionary<char, int>
    {
        ['2'] = 0,
        ['3'] = 1,
        ['4'] = 2,
        ['5'] = 3,
        ['6'] = 4,
        ['7'] = 5,
        ['8'] = 6,
        ['9'] = 7,
        ['T'] = 8,
        ['J'] = 9,
        ['Q'] = 10,
        ['K'] = 11,
        ['A'] = 12
    };

    private static readonly Dictionary<char, int> CardValuesPt2 = new Dictionary<char, int>
    {
        ['J'] = 0,
        ['2'] = 1,
        ['3'] = 2,
        ['4'] = 3,
        ['5'] = 4,
        ['6'] = 5,
        ['7'] = 6,
        ['8'] = 7,
        ['9'] = 8,
        ['T'] = 9,
        ['Q'] = 10,
        ['K'] = 11,
        ['A'] = 12
    };

    public static int GetCardValue(this char card, bool partTwo = false)
    {
        return partTwo ? CardValuesPt2[card] : CardValuesPt1[card];
    }
}