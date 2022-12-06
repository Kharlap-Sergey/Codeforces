// See https://aka.ms/new-console-template for more information
int n = Convert.ToInt32(Console.ReadLine());

for(var i = 0; i< n; i++)
{
    int a, b, c;
    var inputs = Console.ReadLine().Split(" ").Select(input => Convert.ToInt32(input)).ToList();
    (a, b, c) = (inputs[0], inputs[1], inputs[2]);

    if (a == b + c || b == a + c || c == b + a)
        Console.WriteLine("YES");
    else
        Console.WriteLine("NO");
}
