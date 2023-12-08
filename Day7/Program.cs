using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

Console.WriteLine("Input your file path:");
List<string> lines = File.ReadAllText(Console.ReadLine()).Trim().Split('\n').ToList();

int sum1 = 0;
int sum2 = 0;

List<Hand> hands = new List<Hand>();

foreach (string line in lines)
{
    List<Card> cards = new List<Card>();
    line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0].ToCharArray().ToList().ForEach((element) =>
    {
        cards.Add(new Card(element.ToString()));
    });
    int bid=int.Parse(line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
    hands.Add(new Hand(cards, bid, false));
}

//Part1
hands = hands.OrderBy(e => e.HandType)
                   .ThenBy(e => e.Cards[0].getCardValue(false))
                   .ThenBy(e => e.Cards[1].getCardValue(false))
                   .ThenBy(e => e.Cards[2].getCardValue(false))
                   .ThenBy(e => e.Cards[3].getCardValue(false))
                   .ThenBy(e => e.Cards[4].getCardValue(false))
                   .ToList();

hands.Select((e, index) => { e.Rank = index + 1; return e; }).ToList();

sum1 = hands.Sum(e => e.Winning);
Console.WriteLine(sum1);

//Part2

hands.Clear();

foreach (string line in lines)
{
    List<Card> cards = new List<Card>();
    line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0].ToCharArray().ToList().ForEach((element) =>
    {
        cards.Add(new Card(element.ToString()));
    });
    int bid = int.Parse(line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
    hands.Add(new Hand(cards, bid, true));
}


hands.Select(hand => { hand.getHandType(hand.Cards, true); return hand; });
hands = hands.OrderBy(e => e.HandType)
                   .ThenBy(e => e.Cards[0].getCardValue(true))
                   .ThenBy(e => e.Cards[1].getCardValue(true))
                   .ThenBy(e => e.Cards[2].getCardValue(true))
                   .ThenBy(e => e.Cards[3].getCardValue(true))
                   .ThenBy(e => e.Cards[4].getCardValue(true))
                   .ToList();

hands.Select((e, index) => { e.Rank = index + 1; return e; }).ToList();

sum2 = hands.Sum(e => e.Winning);

Console.WriteLine(sum2);

enum HandType
{
    Highcard,OnePair,TwoPair,ThreeOfAKind,FullHouse,FourOfAKind,FiveOfAKind
}

class Card
{
    public string Name;
    public int Value;

    public Card(string name)
    {
        Name = name;
    }

    public int getCardValue(bool jIsJoker)
    {
        if (int.TryParse(Name, out int res))
        {
            return res;
        }
        else
        {
            switch (Name)
            {
                case "A": return 14;
                case "K": return 13;
                case "Q": return 12;
                case "J": return jIsJoker ? 1 : 11;
                case "T": return 10;
            }
        }
        return 0;
    }
}


class Hand
{
    public HandType HandType;
    public List<Card> Cards;
    public int Rank=0;
    public int Bid=0;
    public int Winning { get { return Bid * Rank; }}

    public Hand(List<Card> cards, int bid, bool isPart2)
    {
        Cards = cards;
        getHandType(cards, isPart2);
        Bid = bid;
    }

    public void getHandType(List<Card> cards, bool isPart2) 
    {
        Dictionary<char, int> labelCount = new Dictionary<char, int>();

        foreach (Card card in cards)
        {
            char label = Convert.ToChar(card.Name);
            if (labelCount.ContainsKey(label))
                labelCount[label]++;
            else
                labelCount[label] = 1;
        }
        int distinctLabels = labelCount.Count;
        switch (distinctLabels)
        {
            case 1:
                HandType = HandType.FiveOfAKind;
                break;
            case 2:
                HandType = labelCount.ContainsValue(4) ? HandType.FourOfAKind : HandType.FullHouse;
                break;
            case 3:
                HandType = labelCount.ContainsValue(3) ? HandType.ThreeOfAKind : HandType.TwoPair;
                break;
            case 4:
                HandType = HandType.OnePair;
                break;
            case 5:
                HandType = HandType.Highcard; 
                break;
        }
        if (!isPart2)
            return;
        if(cards.Any(e=> e.Name == "J"))
        {
            if (HandType == HandType.FourOfAKind)
                HandType = HandType.FiveOfAKind;
            else if (HandType == HandType.FullHouse)
            {
                if (cards.Count(e => e.Name == "J") == 1)
                    HandType = HandType.FourOfAKind;
                else
                    HandType = HandType.FiveOfAKind;
            }
            else if (HandType == HandType.ThreeOfAKind)
                HandType = HandType.FourOfAKind;
            else if (HandType == HandType.TwoPair)
            {
                if (cards.Count(e => e.Name == "J") == 1)
                    HandType = HandType.FullHouse;
                else
                    HandType = HandType.FourOfAKind;
            }
            else if (HandType == HandType.OnePair) 
                HandType = HandType.ThreeOfAKind;
            else if (HandType == HandType.Highcard)
                HandType = HandType.OnePair;
        }
    }
}
