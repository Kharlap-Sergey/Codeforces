var ReadInt = () => Convert.ToInt32(Console.ReadLine());
var ReadInts = () => Console.ReadLine().Split().Select(inp => Convert.ToInt64(inp)).ToList();
var main = () =>
{
    var t = ReadInt();
    int n = 0;
    for (int i = 0; i < t; i++)
    {
        n = ReadInt();
        var vals = ReadInts();
        var ps = new ProblemSolver(n, vals);
        foreach(var res in ps.Solve())
        {
            Console.Write($"{res} ");
        }
        Console.WriteLine();
    }
};

main();

public class ProblemSolver
{
    private readonly int _n;
    private readonly List<long> _vals;
    private bool _isInited;
    private Dictionary<int, List<long>> _bitPosNumbers;
    private Dictionary<long, int> _availableNumbers;
    private long _max;

    public ProblemSolver(int n, List<long> vals)
    {
        _n = n;
        _vals = vals;
    }

    public void Init()
    {
        _isInited = true;

        _bitPosNumbers = new Dictionary<int, List<long>>();
        _availableNumbers = new Dictionary<long, int>();
        _max = -1;
        foreach (var val in _vals)
        {
            _max = Math.Max(_max, val);
            if (!_availableNumbers.ContainsKey(val)) _availableNumbers[val] = 0;
            _availableNumbers[val]++;

            var bin = Convert.ToString(val, 2);
            for (int i = 0; i < bin.Length; i++)
            {
                if (bin[i] == '1')
                {
                    if (!_bitPosNumbers.ContainsKey(bin.Length - i - 1)) _bitPosNumbers[bin.Length - i - 1] = new List<long>();

                    _bitPosNumbers[bin.Length - i - 1].Add(val);
                }
            }
        }
    }
    public List<long> Solve()
    {
        if (!_isInited) Init();
        _isInited = false;

        var res = new List<long>();
        var current = _max;
        var currentBin = Convert.ToString(current, 2);
        res.Add(current);
        _availableNumbers[current]--;

        var bitPos = 0;
        long partialMax = 0;
        long partialMaxNumber = 0;
        for(int i = 0; i < currentBin.Length; i++)
        {
            if (currentBin[i] == '1') continue;
            bitPos = currentBin.Length - i - 1;

            if (!_bitPosNumbers.ContainsKey(bitPos)) continue;

            partialMax = current;
            partialMaxNumber = 0;
            foreach (var val in _bitPosNumbers[bitPos])
            {
                if(partialMax < (current | val))
                {
                    partialMaxNumber = val;
                    partialMax = current | val;
                }
            }

            current |= partialMaxNumber;
            currentBin = Convert.ToString(current, 2);
            res.Add(partialMaxNumber);
            _availableNumbers[partialMaxNumber]--;
        }
        
        foreach(var kvp in _availableNumbers)
        {
            for(int k = 0; k < kvp.Value; k++)
                res.Add(kvp.Key);
        }

        return res;
    }
}

