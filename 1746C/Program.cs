var ReadInt = () => Convert.ToInt32(Console.ReadLine());
var ReadInts = () => Console.ReadLine().Split().Select(inp => Convert.ToInt32(inp)).ToList();
var t = ReadInt();

for (int tt = 0; tt < t; tt++)
{
    var n = ReadInt();
    var numbers = ReadInts();
    var res = new int[n];

    for(var i = 0; i < n; i++)
    {
        res[n - numbers[i]] = i + 1;
    }

    for(var i = 0; i < n; i++)
    {
        Console.Write($"{res[i]} ");
    }
    Console.WriteLine();
}