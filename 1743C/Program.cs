var ReadInt = () => Convert.ToInt32(Console.ReadLine());
var ReadInts = () => Console.ReadLine().Split().Select(inp => Convert.ToInt32(inp)).ToList();
var main = () =>
{
    var t = ReadInt();
    int n = 0;
    for (int tt = 0; tt < t; tt++)
    {
        n = ReadInt();
        var clousers = Console.ReadLine();
        var vals = ReadInts();

        var res =  0;
        var i = 0;
        while(i < n)
        {
            if(clousers[i] == '1')
            {
                res += vals[i];
                i++;
                continue;
            }

            var j = i + 1;
            var min = vals[i];
            var sum = vals[i];
            while (j < n && clousers[j] == '1')
            {
                min = Math.Min(vals[j], min);
                sum += vals[j];
                j++;
            }

            res += sum - min;
            i = j;
        }

        Console.WriteLine(res);
    }
};

main();

