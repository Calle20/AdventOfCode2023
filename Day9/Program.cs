Console.WriteLine("Input your file path:");
List<string> lines = File.ReadAllText(Console.ReadLine()).Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();

int sum1 = 0;
int sum2 = 0;

List<List<int>> historys = lines.Select(e => e.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();


foreach (List<int> history in historys)
{
    List<List<int>> differencesList=new List<List<int>>() { history };
    do
    {
        differencesList.Add(Enumerable.Range(0, differencesList.Last().Count-1)
            .Select(i => differencesList.Last()[i+1] - differencesList.Last()[i])
            .ToList());
    } while (!differencesList.Last().TrueForAll(e=>e.Equals(0)));

    differencesList.Last().Add(0);

    differencesList.Reverse();
    foreach(List<int> line in differencesList.Skip(1))
    {
        line.Add(line.Last()+differencesList[differencesList.IndexOf(line) - 1].Last());
    }
    sum1 += differencesList.Last().Last();
}

Console.WriteLine(sum1);

foreach (List<int> history in historys)
{
    List<List<int>> differencesList = new List<List<int>>() { history };
    do
    {
        differencesList.Add(Enumerable.Range(0, differencesList.Last().Count - 1)
            .Select(i => differencesList.Last()[i + 1] - differencesList.Last()[i])
            .ToList());
    } while (!differencesList.Last().TrueForAll(e => e.Equals(0)));

    differencesList.Last().Add(0);

    differencesList.Reverse();
    foreach (List<int> line in differencesList.Skip(1))
    {
        line.Insert(0,line.First() - differencesList[differencesList.IndexOf(line) - 1].First());
    }
    sum2 += differencesList.Last().First();
}

Console.WriteLine(sum2);