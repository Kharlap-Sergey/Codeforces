var t = Convert.ToInt32(Console.ReadLine());

for(var tt = 0; tt < t; tt++)
{
    var n = Convert.ToInt32(Console.ReadLine());

    long countAinFirst = 1;
    long countAinSecond = 1;
    long countNonAinFirst = 0;
    long countNonAinSecond = 0;
    for(var i = 0; i < n; i++)
    {
        var input  = Console.ReadLine().Split();

        var type = input[0];
        var multiplication = Convert.ToInt32(input[1]);

        foreach(var character in input[2])
        {
            if(type == "1")
            {
                if (character == 'a')
                    countAinFirst += multiplication;
                else countNonAinFirst += multiplication;
            }
            else
            {
                if (character == 'a')
                    countAinSecond += multiplication;
                else countNonAinSecond += multiplication;
            }
        }

        if(countNonAinSecond > 0)
        {
            Console.WriteLine("YES");
            continue;
        }

        if(countNonAinFirst > 0 || countAinFirst >= countAinSecond)
        {
            Console.WriteLine("NO");
            continue;
        }

        Console.WriteLine("YES");
    }
}