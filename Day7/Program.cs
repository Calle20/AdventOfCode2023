using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

Console.WriteLine("Input your file path:");
List<string> lines = File.ReadAllText(Console.ReadLine()).Trim().Split('\n').ToList();

int sum1 = 0;
int sum2 = 0;

List<Hand> hands = new List<Hand>();
List<int> bids=new List<int>();

foreach (string line in lines)
{
    List<Card> cards = new List<Card>();
    line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0].ToCharArray().ToList().ForEach((element) =>
    {
        cards.Add(new Card(element.ToString()));
    });
    int bid=int.Parse(line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
    hands.Add(new Hand(cards, bid));
}

hands = hands.OrderBy(e => e.HandType)
                   .ThenBy(e => e.Cards[0].Value)
                   .ThenBy(e => e.Cards[1].Value)
                   .ThenBy(e => e.Cards[2].Value)
                   .ThenBy(e => e.Cards[3].Value)
                   .ThenBy(e => e.Cards[4].Value)
                   .ToList();

hands.Select((e, index) => { e.Rank = index + 1; return e; }).ToList();

sum1 = hands.Sum(e => e.Winning);
Console.WriteLine(sum1);
Console.WriteLine(sum2);

enum HandType
{
    Highcard,OnePair,TwoPair,ThreeOfAKind,FullHouse,FourOfAKind,FiveOfAKind
}

class Card
{
    public string Name;
    public int Value { 
        get {
            if(int.TryParse(Name, out int res))
            {
                return res-2;
            }
            else
            {
                switch (Name)
                {
                    case "A": return 12;
                    case "K": return 11;
                    case "Q": return 10;
                    case "J": return 9;
                    case "T": return 8;
                }
            }
            return 0;
        } 
    }

    public Card(string name)
    {
        Name = name;
    }
}


class Hand
{
    public HandType HandType;
    public List<Card> Cards;
    public int Rank=0;
    public int Bit=0;
    public int Winning { get { return Bit * Rank; }}

    public Hand(List<Card> cards, int bit)
    {
        Cards = cards;
        HandType = getHandType(cards);
        Bit = bit;
    }

    private HandType getHandType(List<Card> cards) 
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
                return HandType.FiveOfAKind;
            case 2:
                return labelCount.ContainsValue(4) ? HandType.FourOfAKind : HandType.FullHouse;
            case 3:
                return labelCount.ContainsValue(3) ? HandType.ThreeOfAKind : HandType.TwoPair;
            case 4:
                return HandType.OnePair;
            default:
                return HandType.Highcard;
        }
    }
}
