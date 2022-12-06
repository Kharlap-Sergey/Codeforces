using System.Text;

var ReadInt = () => Convert.ToInt32(Console.ReadLine());
var ReadInts = () => Console.ReadLine().Split().Select(inp => Convert.ToInt32(inp)).ToList();
var main = () =>
{
    var n = ReadInt();
    var str = Console.ReadLine();

    var ps = new ProblemSolver(n, str);
    Console.WriteLine(ps.Solve());
};

main();


public class ProblemSolver
{
    private readonly int _n;
    private readonly string _s;

    public ProblemSolver(int n, string s)
    {
        _n = n;
        _s = s;
    }

    public string Solve()
    {
        var ignorePrefix = -1;
        var firstZerroPos = _n;
        for (int i = 0; i < _n; i++)
            if (_s[i] == '1')
            {
                ignorePrefix = i;
                break;
            }

        if (ignorePrefix == -1)
        {
            return "0";
        }

        for (int i = ignorePrefix; i < _n; i++)
            if (_s[i] == '0')
            {
                firstZerroPos = i;
                break;
            }

        var strBuilder = new StringBuilder();
        for (var i = 0; i < firstZerroPos - ignorePrefix; i++)
            strBuilder.Append("1");

        if(firstZerroPos == _n)
            return strBuilder.ToString();

        var startIndex = ignorePrefix;
        var maxzerrosPos = new List<int> { firstZerroPos };
        for(var i = ignorePrefix; i < firstZerroPos; i++)
        {
            var zerrosPos = new List<int>();
            for(var j = 0; j < _n - firstZerroPos; j++)
            {
                var s1 = _s[i + j];
                var s2 = _s[j + firstZerroPos];

                
                if (s1 == '1' || s2 == '1') continue;

                zerrosPos.Add(j + firstZerroPos);
            }

            if(Compare(maxzerrosPos, zerrosPos) < 0) 
            {
                maxzerrosPos = zerrosPos;
                startIndex = i;
            }
        }


        for (var j = 0; j < _n - firstZerroPos; j++)
        {
            var s1 = _s[startIndex + j];
            var s2 = _s[j + firstZerroPos];

            if (s1 == '1' || s2 == '1') strBuilder.Append("1");
            else strBuilder.Append("0");
        }

        return strBuilder.ToString();
    }

    private int Compare(List<int> a, List<int> b)
    {
        var comp = 0;
        for (var j = 0; j < Math.Min(a.Count, b.Count); j++)
        {
            if (a[j] == b[j]) continue;
            else
            {
                comp = a[j] - b[j];
                break;
            }
        }

        if (comp != 0) return comp;

        return b.Count - a.Count;
    }
}
