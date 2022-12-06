var ReadInt = () => Convert.ToInt32(Console.ReadLine());
var ReadInts = () => Console.ReadLine().Split().Select(inp => Convert.ToInt64(inp)).ToList();
var main = () =>
{
    var t = ReadInt();
    int n = 0;
    for (int i = 0; i < t; i++)
    {
        n = ReadInt();
        ReadInts();
        Console.WriteLine((10 - n) * 3 * (9 - n));
    }
};

main();

