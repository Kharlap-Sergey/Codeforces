var ReadInt = () => Convert.ToInt32(Console.ReadLine());
var ReadInts = () => Console.ReadLine().Split().Select(inp => Convert.ToInt64(inp)).ToList();
var main = () =>
{
    var t = ReadInt();
    int n = 0;
    for (int i = 0; i < t; i++)
    {
        n = ReadInt();
        var temp = 1;
        while(temp <= n)
        {
            Console.Write($"{temp} ");
            temp += 2;
        }
        temp = (n / 2) * 2;
        while(temp >= 1)
        {
            Console.Write($"{temp} ");
            temp -= 2;
        }
        Console.WriteLine();
    }
};

main();

