var t = Convert.ToInt32(Console.ReadLine());

for (var tt = 0; tt < t; tt++)
{
    var n = Console.ReadLine();

    var input = Console.ReadLine().Split();

    var zeroFromBackIndex = input.Length - 1;

    var count = 0;
    while (zeroFromBackIndex >= 0 && input[zeroFromBackIndex] != "0") zeroFromBackIndex--;

    for(int i = 0; i < input.Length; i++)
    {
        if (zeroFromBackIndex < i)
            break;
        if (input[i] == "0")
            continue;

        count++;
        zeroFromBackIndex--;
        while (zeroFromBackIndex >= 0 && input[zeroFromBackIndex] != "0") zeroFromBackIndex--;
    }

    Console.WriteLine(count);
}