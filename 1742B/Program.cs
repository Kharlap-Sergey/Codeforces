// See https://aka.ms/new-console-template for more information
int t = Convert.ToInt32(Console.ReadLine());

for (var i = 0; i < t; i++)
{
    int n = Convert.ToInt32(Console.ReadLine());

    var collection = Console.ReadLine().Split();
    var set = new HashSet<string>();
    var isValid = true;
    foreach(var item in collection)
    {
        if(set.Contains(item))
        {
            isValid = false;
            break;
        }    
        else
            set.Add(item);
    }

    if (isValid)
        Console.WriteLine("Yes");
    else
        Console.WriteLine("no");
}
