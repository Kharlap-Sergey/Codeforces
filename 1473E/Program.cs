using System.Text;

var ReadInt = () => Convert.ToInt32(Console.ReadLine());
var ReadInts = () => Console.ReadLine().Split().Select(inp => Convert.ToInt32(inp)).ToList();
var main = () =>
{
    var inputs = ReadInts();
    int p1 = inputs[0], t1 = inputs[1];
    inputs = ReadInts();
    int p2 = inputs[0], t2 = inputs[1];
    inputs = ReadInts();
    int h = inputs[0], s = inputs[1];

    var H = new long[h + 20];
    var T1 = new long[h + 20];
    var T2 = new long[h + 20];

    for(int i = 0; i <= h; i++)
    {
        H[i] = long.MaxValue;
    }

    H[h] = 0;
    T1[h] = t1;
    T2[h] = t2;
    var tmax = Math.Max(t1, t2);
    long newHeals = 0;
    for(int i = h; i > 0; i--)
    {
        if (H[i] == long.MaxValue) continue;

        //1
        newHeals = Math.Max(0, i - p1 + s);
        if(H[newHeals] > Math.Max(T1[i], H[i] + t1)){
            H[newHeals] = Math.Max(T1[i], H[i] + t1);
            T1[newHeals] = H[newHeals] + t1;
            T2[newHeals] = T2[i];
        }
        else if (H[newHeals] == Math.Max(T1[i], H[i] + t1) 
        && (T1[newHeals] > H[newHeals] + t1 || T2[newHeals] > T2[i])
        )
        {
            H[newHeals] = Math.Max(T1[i], H[i] + t1);
            T1[newHeals] = H[newHeals] + t1;
            T2[newHeals] = T2[i];
        }
        //2
        newHeals = Math.Max(0, i - p2 + s);
        if (H[newHeals] > Math.Max(T2[i], H[i] + t2))
        {
            H[newHeals] = Math.Max(T2[i], H[i] + t2);
            T2[newHeals] = H[newHeals] + t2;
            T1[newHeals] = T1[i];
        }
        else if (H[newHeals] == Math.Max(T2[i], H[i] + t2) 
        && (T2[newHeals] > H[newHeals] + t2 || T1[newHeals] > T1[i])
        )
        {
            H[newHeals] = Math.Max(T2[i], H[i] + t2);
            T2[newHeals] = H[newHeals] + t2;
            T1[newHeals] = T1[i];
        }
        //3
        newHeals = Math.Max(0, i - p2 - p1 + s);
        if (H[newHeals] > Math.Max(Math.Min(T2[i], H[i] + t2), Math.Min(T1[i], H[i] + t1)))
        {
            H[newHeals] = Math.Max(Math.Min(T2[i], H[i] + t2), Math.Min(T1[i], H[i] + t1));
            T2[newHeals] = H[newHeals] + t2;
            T1[newHeals] = H[newHeals] + t1;
        }
        else if (H[newHeals] == Math.Max(Math.Min(T2[i], H[i] + t2), Math.Min(T1[i], H[i] + t1))
        && Math.Max(T2[newHeals], T1[newHeals]) > Math.Max(H[newHeals] + t1, H[newHeals] + t2)
        )
        {
            H[newHeals] = Math.Max(Math.Min(T2[i], H[i] + t2), Math.Min(T1[i], H[i] + t1));
            T2[newHeals] = H[newHeals] + t2;
            T1[newHeals] = H[newHeals] + t1;
        }
        else if (H[newHeals] == Math.Max(Math.Min(T2[i], H[i] + t2), Math.Min(T1[i], H[i] + t1))
        && Math.Max(T2[newHeals], T1[newHeals]) == Math.Max(H[newHeals] + t1, H[newHeals] + t2)
        && Math.Min(T2[newHeals], T1[newHeals]) > Math.Min(H[newHeals] + t1, H[newHeals] + t2)
        )
        {
            H[newHeals] = Math.Max(Math.Min(T2[i], H[i] + t2), Math.Min(T1[i], H[i] + t1));
            T2[newHeals] = H[newHeals] + t2;
            T1[newHeals] = H[newHeals] + t1;
        }
    }

    Console.WriteLine(H[0]);
};

main();

public class Dynamic
{
    public int Health { get; set; }

    public long T1 { get; set; }

    public long T2 { get; set; }
}
