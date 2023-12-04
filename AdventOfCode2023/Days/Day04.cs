using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days;

public class Day04(string inputFileName) : AocChallenge(inputFileName)
{
    public override void Part01()
    {
        var lines = Regex.Split(ChallengeFileString, "\r\n|\r|\n");
        var cards = lines.Select(l => new Card(l));

        var cardPointSum = 0;

        foreach (var card in cards)
        {
            var cardPoints = 0;

            foreach (var number in card.NumbersWeHave)
            {
                if (card.WinningNumbers.Contains(number))
                {
                    cardPoints = cardPoints == 0 ? 1 : cardPoints * 2;
                }
            }

            cardPointSum += cardPoints;
        }

        Console.WriteLine($"[DAY04][PT1] Total card points: {cardPointSum}");
    }

    public override void Part02()
    {
        var lines = Regex.Split(ChallengeFileString, "\r\n|\r|\n");
        var cards = lines.Select(l => new Card(l)).ToArray();

        for (var iCard = 0; iCard < cards.Length; iCard++)
        {
            var card = cards[iCard];
            var winningNumbers = card.NumbersWeHave.Count(c => card.WinningNumbers.Contains(c));

            // Increase the copy count of the next X cards by the amount of copies our current card has
            for (var iCopyCard = iCard + 1; iCopyCard <= iCard + winningNumbers; iCopyCard++)
            {
                cards[iCopyCard].Copies += card.Copies;
            }
        }

        var numScratchCards = cards.Aggregate(0, (cardCount, card) => cardCount + card.Copies);

        Console.WriteLine($"[DAY04][PT2] Total number of cards: {numScratchCards}");
    }
}

internal class Card
{
    public Card(string line)
    {
        var lineParts = line.Split(":");

        Number = int.Parse(lineParts[0].Split(" ")[^1]);

        var cardNumbers = lineParts[1].Split("|");
        WinningNumbers = cardNumbers[0].Split(" ").Where(c => c != " " && c != "").Select(n => int.Parse(n.Trim())).ToArray();
        NumbersWeHave = cardNumbers[1].Split(" ").Where(c => c != " " && c != "").Select(n => int.Parse(n.Trim())).ToArray();
    }

    public int Copies { get; set; } = 1;

    public int Number { get; set; }
    public int[] WinningNumbers { get; set; }
    public int[] NumbersWeHave { get; set; }
}