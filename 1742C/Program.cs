// See https://aka.ms/new-console-template for more information
int t = Convert.ToInt32(Console.ReadLine());

for (var i = 0; i < t; i++)
{

    var isRed = false;
    Console.ReadLine();
    for (var j = 0; j < 8; j++)
    {
        var line = Console.ReadLine();
        isRed = isRed || line == "RRRRRRRR";
    }

    if (isRed)
        Console.WriteLine("R");
    else
        Console.WriteLine("B");
}
