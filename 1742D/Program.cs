var t = Convert.ToInt32(Console.ReadLine());

for (int tt = 0; tt < t; tt++)
{
    var n = Console.ReadLine();
    var numbers = Console.ReadLine().Split().Select(inp => Convert.ToInt32(inp)).ToList();

    var uniqNumbersWithPositions= new Dictionary<int, int>();
    var previousOnePos = -1;
    for(var j = 0; j < numbers.Count; j++)
    {
        if (numbers[j] == 1)
            previousOnePos = j + 1;

        uniqNumbersWithPositions[numbers[j]] = j + 1;
    }

    var res = new List<(int, int)>();
    
    if(previousOnePos > 0) res.Add((1, previousOnePos));
    foreach(var kvp in uniqNumbersWithPositions)
    {
        res.Add((kvp.Key, kvp.Value));
    }

    var max = -1;
    for (int i = 0; i < res.Count-1; i++)
    {
        for (int j = i+1; j < res.Count; j++)
        {
            if (res[i].Item1.IsCoprime(res[j].Item1))
                max = Math.Max(res[i].Item2 + res[j].Item2, max);
        }
    }
    Console.WriteLine(max);
}

public static class MathNumericHelper
{
    public static bool IsCoprime(this int a, int b)
    {
        return FindNod(a, b) == 1;
    }

    private static int FindNod(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a + b;
    }
}