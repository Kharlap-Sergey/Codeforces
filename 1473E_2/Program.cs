using System.Text;

var ReadInt = () => Convert.ToInt32(Console.ReadLine());
var ReadInts = () => Console.ReadLine().Split().Select(inp => Convert.ToInt64(inp)).ToList();
var main = () =>
{
    var inputs = ReadInts();
    int p1 = (int)inputs[0]; long t1 = inputs[1];
    inputs = ReadInts();
    int p2 = (int)inputs[0]; long t2 = inputs[1];
    inputs = ReadInts();
    int h = (int)inputs[0], s = (int)inputs[1];

    var ps = new ProblemSolver(
        p1, t1,
        p2, t2,
        h, s
        );

    Console.WriteLine(ps.Solve());
};

main();

public class ProblemSolver
{
    private readonly int _p1;
    private readonly long _t1;
    private readonly int _p2;
    private readonly long _t2;
    private readonly int _h;
    private readonly int _s;
    Dictionary<int, List<Dynamic>> _dynamics = new Dictionary<int, List<Dynamic>>();
    private readonly DynamicComparer _dynamicComparer = new DynamicComparer();
    public ProblemSolver(
        int p1, long t1,
        int p2, long t2,
        int h, int s
        )
    {
        _p1 = p1;
        _t1 = t1;
        _p2 = p2;
        _t2 = t2;
        _h = h;
        _s = s;
    }

    private int ShouldGo(int to, Dynamic with)
    {
        if (_dynamics[to].Count == 0)
            return 0;

        var ignoredCount = 0;
        var f = false;
        foreach (var d in _dynamics[to])
            if (d.Ignore || 
                (Math.Max(d.T1, d.Time) >= Math.Max(with.T1, with.Time) 
                && Math.Max(d.T2, d.Time) >= Math.Max(with.T2, with.Time))
                )
            {
                d.Ignore = true;
                ignoredCount++;
            }
            else
            if (
                Math.Max(d.T1, d.Time) <= Math.Max(with.T1, with.Time) 
                && Math.Max(d.T2, d.Time) <= Math.Max(with.T2, with.Time)
                )
            {
                f = true;
            }

        if (ignoredCount == _dynamics[to].Count)
            return -1;

        //if(true)
        //{
        //    var temp = new List<Dynamic>();
        //    foreach (var d in _dynamics[to])
        //        if (!d.Ignore)
        //            temp.Add(d);

        //    _dynamics[to] = temp;
        //}
        if (f) return 1;
        return 0;
    }

    public long Solve()
    {
        _dynamics[_h] = new List<Dynamic>();
        _dynamics[_h].Add(new Dynamic
        {
            Time = 0,
            T1 = _t1,
            T2 = _t2
        });

        int newHeals = 0;
        long T1, T2, time, lowTime, stepDes;
        Dynamic newD;
        for (int i = _h; i > 0; i--)
        {
            if (!_dynamics.ContainsKey(i)) continue;

            //1
            newHeals = Math.Max(0, i - _p1 + _s);
            if (!_dynamics.ContainsKey(newHeals)) _dynamics[newHeals] = new List<Dynamic>();
            newHeals = Math.Max(0, i - _p2 + _s);
            if (!_dynamics.ContainsKey(newHeals)) _dynamics[newHeals] = new List<Dynamic>();
            newHeals = Math.Max(0, i - _p1 - _p2 + _s);
            if (!_dynamics.ContainsKey(newHeals)) _dynamics[newHeals] = new List<Dynamic>();

            foreach (var d in _dynamics[i])
            {
                //if (ShouldGo(i, d) == 1) continue;
                if (d.Ignore) continue;

                T1 = d.T1;
                T2 = d.T2;
                time = d.Time;
                newHeals = Math.Max(0, i - _p1 + _s);
                lowTime = Math.Max(T1, time);
                newD = new Dynamic
                {
                    Time = lowTime,
                    T2 = T2,
                    T1 = _t1 + lowTime,
                };
                stepDes = ShouldGo(newHeals, newD);
                //if (_dynamics[newHeals].Count == 0 || _dynamics[newHeals][0].Time > lowTime)
                if(stepDes == -1)
                {
                    _dynamics[newHeals] = new List<Dynamic>
                    {
                        newD
                    };
                }
                //else 
                //if (_dynamics[newHeals].Count == 0 || _dynamics[newHeals][0].Time >= lowTime)
                if (stepDes == 0)
                {
                    _dynamics[newHeals].Add(
                       newD
                    );
                }

                newHeals = Math.Max(0, i - _p2 + _s);
                lowTime = Math.Max(time, T2);
                newD = new Dynamic
                {
                    Time = lowTime,
                    T2 = _t2 + lowTime,
                    T1 = T1,
                };
                stepDes = ShouldGo(newHeals, newD);

                //if (_dynamics[newHeals].Count == 0 || _dynamics[newHeals][0].Time > lowTime)
                if (stepDes == -1)
                {
                    _dynamics[newHeals] = new List<Dynamic>
                    {
                        newD
                    };
                }
                //else 
                //if (_dynamics[newHeals].Count == 0 || _dynamics[newHeals][0].Time >= lowTime)
                else if (stepDes == 0)
                {
                    _dynamics[newHeals].Add(
                        newD
                    );
                }

                newHeals = Math.Max(0, i - _p2 - _p1 + _s);
                lowTime = Math.Max(time, Math.Max(T1, T2));
                newD = new Dynamic
                {
                    Time = lowTime,
                    T2 = _t2 + lowTime,
                    T1 = _t1 + lowTime,
                };
                stepDes = ShouldGo(newHeals, newD);
                if (stepDes == -1)
                //if (_dynamics[newHeals].Count == 0 
                //    || _dynamics[newHeals][0].Time > lowTime)
                {
                    _dynamics[newHeals] = new List<Dynamic>
                    {
                       newD
                    };
                }
                //else 
                //if (_dynamics[newHeals].Count == 0
                //    || _dynamics[newHeals][0].Time >= lowTime)
                if (stepDes == 0)
                {
                    _dynamics[newHeals].Add(
                    newD
                );
                }
            }
        }

        return _dynamics[0].Select(t => t.Time).Min();
    }
}

public class Dynamic
{
    public long Time { get; set; }

    public long T1 { get; set; }

    public long T2 { get; set; }

    public bool Ignore { get; set; } = false;
}

public class DynamicComparer : IComparer<Dynamic>
{
    public int Compare(Dynamic? x, Dynamic? y)
    {
        if (x.T1 == y.T1 && y.T2 == x.T2) return 0;
        if (x.T1 <= y.T1 && x.T2 >= y.T2) return 0;
        if (x.T1 >= y.T1 && x.T2 <= y.T2) return 0;
        return x.T1 > y.T1 && x.T2 > y.T2 ? x.Time < y.Time ? 0 : 1 : x.Time > y.Time ? 0 : -1;
    }
}