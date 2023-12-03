Console.WriteLine("Input your file path:");
string path = Console.ReadLine();
string data = File.ReadAllText(path);

data = data.Trim();

string[] lines = data.Split('\n');
int sum1 = 0;
int sum2 = 0;

int height=lines.Length;
int width = lines[0].ToCharArray().Length;

List<List<char>> engineGrid = new List<List<char>>(width*height);
List<Gear> gears = new List<Gear>();

for (int i=0;i<height;i++)
{
    engineGrid.Add(new List<char>());
    string line = lines[i];
    char[]chars = line.ToCharArray();
    for(int j = 0; j < chars.Length; j++)
    {
        engineGrid[i].Add(chars[j]);
    }
}

for(int i = 0; i < width; i++)
{
    for(int j = 0; j < height; j++)
    {
        if (char.IsDigit(engineGrid[i][j]))
        {
            bool isPartNumber = false;
            bool hasGear=false;
            Gear gear = new Gear(0, 0, 0);
            string num = engineGrid[i][j].ToString();
            if (
                (i - 1 >= 0 && j - 1 >= 0 && !char.IsDigit(engineGrid[i - 1][j - 1]) && engineGrid[i - 1][j - 1].ToString() != ".") ||
                (i - 1 >= 0 && !char.IsDigit(engineGrid[i - 1][j]) && engineGrid[i - 1][j].ToString() != ".") ||
                (i - 1 >= 0 && j + 1 < engineGrid[i - 1].Count && !char.IsDigit(engineGrid[i - 1][j + 1]) && engineGrid[i - 1][j + 1].ToString() != ".") ||

                (j - 1 >= 0 && !char.IsDigit(engineGrid[i][j - 1]) && engineGrid[i][j - 1].ToString() != ".") ||
                (j + 1 < engineGrid[i].Count && !char.IsDigit(engineGrid[i][j + 1]) && engineGrid[i][j + 1].ToString() != ".") ||

                (i + 1 < engineGrid.Count && j - 1 >= 0 && !char.IsDigit(engineGrid[i + 1][j - 1]) && engineGrid[i + 1][j - 1].ToString() != ".") ||
                (i + 1 < engineGrid.Count && !char.IsDigit(engineGrid[i + 1][j]) && engineGrid[i + 1][j].ToString() != ".") ||
                (i + 1 < engineGrid.Count && j + 1 < engineGrid[i + 1].Count && !char.IsDigit(engineGrid[i + 1][j + 1]) && engineGrid[i + 1][j + 1].ToString() != ".")
            )
            {
                isPartNumber = true;
            }

            if (i - 1 >= 0 && j - 1 >= 0 && engineGrid[i - 1][j - 1].ToString() == "*") 
            {
                hasGear = true;
                gear.X = i - 1;
                gear.Y = j - 1;
            }
            if (i - 1 >= 0 && engineGrid[i - 1][j].ToString() == "*")
            {
                hasGear=true;
                gear.X = i - 1;
                gear.Y = j;
            }
            if(i - 1 >= 0 && j + 1 < engineGrid[i - 1].Count && engineGrid[i - 1][j + 1].ToString() == "*"){
                hasGear=true;
                gear.X = i - 1;
                gear.Y = j + 1;
            }
            if(j - 1 >= 0 && engineGrid[i][j - 1].ToString() == "*")
            {
                hasGear = true;
                gear.X = i;
                gear.Y = j - 1;
            }
            if (j + 1 < engineGrid[i].Count && engineGrid[i][j + 1].ToString() == "*") 
            {
                hasGear = true;
                gear.X = i;
                gear.Y = j + 1;
            }
            if(i + 1 < engineGrid.Count && j - 1 >= 0 && engineGrid[i + 1][j - 1].ToString() == "*"){
                hasGear = true;
                gear.X = i + 1;
                gear.Y = j - 1;
            }
            if (i + 1 < engineGrid.Count && !char.IsDigit(engineGrid[i + 1][j]) && engineGrid[i + 1][j].ToString() == "*") 
            {
                hasGear = true;
                gear.X = i + 1;
                gear.Y = j;
            }
            if(i + 1 < engineGrid.Count && j + 1 < engineGrid[i + 1].Count && engineGrid[i + 1][j + 1].ToString() == "*")
            {
                hasGear = true;
                gear.X = i + 1;
                gear.Y = j + 1;
            }

            while (j + 1 < engineGrid[i].Count&&char.IsDigit(engineGrid[i][j+1]))
            { 
                j++;
                num += engineGrid[i][j].ToString();
                if (
                    (i - 1 >= 0 && j - 1 >= 0 && !char.IsDigit(engineGrid[i - 1][j - 1]) && engineGrid[i - 1][j - 1].ToString() != ".") ||
                    (i - 1 >= 0 && !char.IsDigit(engineGrid[i - 1][j]) && engineGrid[i - 1][j].ToString() != ".") ||
                    (i - 1 >= 0 && j + 1 < engineGrid[i - 1].Count && !char.IsDigit(engineGrid[i - 1][j + 1]) && engineGrid[i - 1][j + 1].ToString() != ".") ||

                    (j - 1 >= 0 && !char.IsDigit(engineGrid[i][j - 1]) && engineGrid[i][j - 1].ToString() != ".") ||
                    (j + 1 < engineGrid[i].Count && !char.IsDigit(engineGrid[i][j + 1]) && engineGrid[i][j + 1].ToString() != ".") ||

                    (i + 1 < engineGrid.Count && j - 1 >= 0 && !char.IsDigit(engineGrid[i + 1][j - 1]) && engineGrid[i + 1][j - 1].ToString() != ".") ||
                    (i + 1 < engineGrid.Count && !char.IsDigit(engineGrid[i + 1][j]) && engineGrid[i + 1][j].ToString() != ".") ||
                    (i + 1 < engineGrid.Count && j + 1 < engineGrid[i + 1].Count && !char.IsDigit(engineGrid[i + 1][j + 1]) && engineGrid[i + 1][j + 1].ToString() != ".")
                )
                {
                    isPartNumber = true;
                }

                if (i - 1 >= 0 && j - 1 >= 0 && engineGrid[i - 1][j - 1].ToString() == "*")
                {
                    hasGear = true;
                    gear.X = i - 1;
                    gear.Y = j - 1;
                }
                if (i - 1 >= 0 && engineGrid[i - 1][j].ToString() == "*")
                {
                    hasGear = true;
                    gear.X = i - 1;
                    gear.Y = j;
                }
                if (i - 1 >= 0 && j + 1 < engineGrid[i - 1].Count && engineGrid[i - 1][j + 1].ToString() == "*")
                {
                    hasGear = true;
                    gear.X = i - 1;
                    gear.Y = j + 1;
                }
                if (j - 1 >= 0 && engineGrid[i][j - 1].ToString() == "*")
                {
                    hasGear = true;
                    gear.X = i;
                    gear.Y = j - 1;
                }
                if (j + 1 < engineGrid[i].Count && engineGrid[i][j + 1].ToString() == "*")
                {
                    hasGear = true;
                    gear.X = i;
                    gear.Y = j + 1;
                }
                if (i + 1 < engineGrid.Count && j - 1 >= 0 && engineGrid[i + 1][j - 1].ToString() == "*")
                {
                    hasGear = true;
                    gear.X = i + 1;
                    gear.Y = j - 1;
                }
                if (i + 1 < engineGrid.Count && !char.IsDigit(engineGrid[i + 1][j]) && engineGrid[i + 1][j].ToString() == "*")
                {
                    hasGear = true;
                    gear.X = i + 1;
                    gear.Y = j;
                }
                if (i + 1 < engineGrid.Count && j + 1 < engineGrid[i + 1].Count && engineGrid[i + 1][j + 1].ToString() == "*")
                {
                    hasGear = true;
                    gear.X = i + 1;
                    gear.Y = j + 1;
                }
            }
            if (isPartNumber)
            {
                sum1 += int.Parse(num);
            }
            if (hasGear)
            {
                gears.Add(new Gear(gear.X, gear.Y, int.Parse(num)));
            }
        }
    }
}

List<Gear> combinedGears=CombineAndMergeGears(gears);

foreach (Gear gear in combinedGears) 
{ 
    sum2 += gear.Ratio;
}

Console.WriteLine(sum1);
Console.WriteLine(sum2);

List<Gear> CombineAndMergeGears(List<Gear> gearList)
{
    List<Gear> combinedGears = new List<Gear>();
    // Group gears by X and Y values
    var groupedGears = gearList.GroupBy(g => new { g.X, g.Y });

    foreach (var group in groupedGears)
    {
        if (group.Count() > 1)
        {
            var combinedGear = group.First();
            foreach (var gear in group.Skip(1))
            {
                combinedGear.Num2 = gear.Num1;
                combinedGear.Ratio = combinedGear.Num1 * combinedGear.Num2;
            }

            combinedGears.Add(combinedGear);

            gearList.RemoveAll(g => group.Contains(g));
        }
    }
    return combinedGears;
}

class Gear
{
    public int Num1=0;
    public int Num2=0;
    public int Ratio=0;
    public int X = 0;
    public int Y = 0;

    public Gear(int x, int y, int num)
    {
        X = x;
        Y = y;
        Num1 = num;
    }
}