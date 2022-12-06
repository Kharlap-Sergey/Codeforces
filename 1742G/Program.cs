//var p1 = "999815193 913891315 89637645 552575215 272182106 616898365 643895902 464862043 813609979 882012786 633775157 840718190 440784915 812906565 396556092 929687397 613759184 577340747 231292218 754486447 568200496 275716808 782419797 793471797 848018109 5131296 870077360 885036192 251911854 152488799 875900851 620064124 281829308 226156466 714845833 521479097 851042979 724757996 468883872 331094315 844390053 101030146 60357419 800466954 516361734 35634319 208277667 265800563 125055121 339602769 389408066".Split().Select(inp => Convert.ToInt32(inp)).ToList();
//var p2 = "999815193 913891315 89637645 616898365 552575215 633775157 643895902 272182106 464862043 882012786 813609979 440784915 840718190 812906565 396556092 929687397 613759184 577340747 231292218 754486447 568200496 275716808 782419797 793471797 848018109 5131296 870077360 885036192 251911854 152488799 875900851 620064124 281829308 226156466 714845833 521479097 851042979 724757996 468883872 331094315 844390053 101030146 60357419 800466954 516361734 35634319 208277667 265800563 125055121 339602769 389408066".Split().Select(inp => Convert.ToInt32(inp)).ToList();

//var pr1 = 0;
//var pr2 = 0;
//for(long i = 0; i < p1.Count; i++)
//{
//    pr1 |= p1[i];
//    pr2 |= p2[i];
//    Console.WriteLine($"{pr1} {pr2}");
//}

var t = Convert.ToInt32(Console.ReadLine());

for (var tt = 0; tt < t; tt++)
{
    var n = Convert.ToInt32(Console.ReadLine());
    var input = Console.ReadLine().Split().Select(inp => Convert.ToInt32(inp)).ToList();
    var availableNumbers = new Dictionary<long, long>();
    var numbersByBinPosition = new Dictionary<long, List<long>>();
    long max = -1;
    var result = new List<long>();

    foreach(var number in input)
    {
        var basedNumber = Convert.ToString(number, 2);
        max = Math.Max(max, number);
        if (!availableNumbers.ContainsKey(number))
            availableNumbers[number] = 0;
        availableNumbers[number]++;

        for(int i = 0; i < basedNumber.Length; i++)
        {
            if (basedNumber[i] != '1')
                continue;

            if (!numbersByBinPosition.ContainsKey(basedNumber.Length - i))
                numbersByBinPosition[basedNumber.Length - i] = new List<long>();

            numbersByBinPosition[basedNumber.Length - i].Add(number);
        }
    }

    long res = max;
    var binBasedRes = Convert.ToString(max, 2);
    availableNumbers[max]--;
    result.Add(max);

    var length = binBasedRes.Length;
    for (int i = 0; i < length; i++)
    {
        if (binBasedRes[i] == '1')
            continue;

        long partMaxResult = -1;
        long partMaxNumber = -1;
        if (!numbersByBinPosition.ContainsKey(length - i))
            continue;
        foreach(var number in numbersByBinPosition[length - i])
        {
            if (availableNumbers[number] <= 0)
                continue;

            if(partMaxResult < (res | number))
            {
                partMaxResult = res | number;
                partMaxNumber = number;
            }
               
        }

        if(partMaxResult != -1)
        {
            availableNumbers[partMaxNumber]--;
            result.Add(partMaxNumber);
            res |= partMaxNumber;
            binBasedRes = Convert.ToString(max, 2);
        }
    }

    foreach(var kvp in availableNumbers)
    {
        for(var i = 0; i < kvp.Value; i++)
            result.Add(kvp.Key);
    }

    foreach(var resNumber in result)
    {
        Console.Write(resNumber);
        Console.Write(" ");
    }
    Console.WriteLine();
}