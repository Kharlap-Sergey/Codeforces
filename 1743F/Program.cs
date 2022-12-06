
var ReadInt = () => Convert.ToInt32(Console.ReadLine());
var ReadInts = () => Console.ReadLine().Split().Select(inp => Convert.ToInt32(inp)).ToList();
var main = () =>
{
    var n = ReadInt();
    var ranges = new List<(int, int)>();
    for (int i = 0; i < n; i++)
    {
        var input = ReadInts();
        ranges.Add((input[0], input[1]));
    }
    var ps = new ProblemSolver(n, ranges);

    Console.WriteLine(ps.Solve());
};

main();

public class MatrixRangeTree : RangeTree<Matrix>
{
    public MatrixRangeTree(int l, int r) 
        : base(l, r, (a, b) => a*b, (v, a, b) => Matrix.Multiply(v, a, b), (_) => ProblemSolver.ZERO)
    {
    }
}
public class RangeTree<T>
{
    private readonly Func<T, T, T> _calculator;
    private readonly Func<T, T, T, T> _recalculator;
    private readonly Func<int, T> _def;

    public T Value { get; set; }

    public RangeTree<T> Left { get; private set; }

    public RangeTree<T> Right { get; private set; }

    public RangeTree<T>[] Vertexes { get; }

    public int L { get; }

    public int R { get; }

    public int V { get; }

    public RangeTree(int l, int r, Func<T, T, T> calculator, Func<T, T, T, T> recalculator, Func<int, T> def)
        : this(0, l, r, calculator,recalculator, def)
    {

    }

    private RangeTree(int v, int l, int r, Func<T, T, T> calculator, Func<T, T, T, T> recalculator, Func<int, T> def)
    {
        V = v;
        L = l;
        R = r;
        _calculator = calculator;
        _recalculator = recalculator;
        _def = def;
    }

    public void Build()
    {
        int l =L, r = R, v = V;

        if (l == r - 1)
        {
             Value = _def(v);
        }
        else
        {
            int m = (l + r) / 2;
            Left = new RangeTree<T>(v * 2 + 1, l, m, _calculator, _recalculator, _def);
            Right = new RangeTree<T>(v * 2 + 2, m, r, _calculator, _recalculator, _def);
            Left.Build();
            Right.Build();
            Value = _calculator(Left.Value, Right.Value);
        }
    }

    public void Update(int pos, T val)
    {
        int l = L, r = R;

        if (l == r - 1)
        {
            Value = val;
        }
        else
        {
            int m = (l + r) / 2;
            if (pos < m) Left.Update(pos, val);
            else Right.Update(pos, val);
            Value = _recalculator(Value, Left.Value, Right.Value);
        }
    }
}

public class ProblemSolver
{
    public static Matrix ZERO = new Matrix(2, 2, new int[2, 2] { { 3, 0 }, { 1, 2 } });
    public static Matrix ONE = new Matrix(2, 2, new int[2, 2] { { 1, 2 }, { 1, 2 } });

    public const int MOD = 998244353;
    const int NUMBERS = 300004;
    private readonly int _n;
    private readonly List<(int, int)> _ranges;
    private readonly List<(int, int)>[] _invertedRanges;

    private bool _isInit;
    private MatrixRangeTree _root;

    public ProblemSolver(
        int n,
        List<(int, int)> ranges
        )
    {
        _n = n;
        _ranges = ranges;
        _invertedRanges = new List<(int, int)>[NUMBERS];
    }

    private bool IfBelong(int x, (int, int) range)
    {
        return x >= range.Item1 && x <= range.Item2;
    }

    private void Init()
    {
        _isInit = true;
        _root = new MatrixRangeTree(0, _n - 1);
        _root.Build();

        for (int i = 0; i < NUMBERS; i++)
            _invertedRanges[i] = new List<(int, int)>();

        for (int i = 0; i < _n; i++)
        {
            int l = _ranges[i].Item1, r = _ranges[i].Item2;

            _invertedRanges[l].Add((1, i));
            _invertedRanges[r + 1].Add((0, i));
        }
    }

    public int Solve()
    {
        if (!_isInit)
            Init();


        var res = 0;
        int cur = 0;
        
        for (int x = 0; x <= 300000; x++)
        {
            

            foreach (var rangeP in _invertedRanges[x])
            {
                if (rangeP.Item2 == 0) cur = rangeP.Item1;
                else _root.Update(rangeP.Item2 - 1, rangeP.Item1 == 0 ? ZERO : ONE);
            }

            res = (res + _root.Value._Matrix[cur, 1]) % MOD;
        }

        return res;
    }
}

public struct Matrix
{
    public Matrix(int n, int m, int[,] matrix)
    {
        RowsCount = n;
        ColumnsCount = m;
        _Matrix = matrix;
    }

    public int RowsCount { get; }
    public int ColumnsCount { get; }
    public int[,] _Matrix { get; }

    int[,] hardMultiplication = new int[2, 2];

    public static Matrix Multiply(Matrix V, Matrix A, Matrix B)
    {
        for(int i = 0; i < 2; i++)
            for(int j = 0; j < 2; j++)
            {
                V._Matrix[i, j] = 0;
            }

        for (int i = 0; i < A.RowsCount; i++)
            for (int j = 0; j < B.ColumnsCount; j++)
                for (int k = 0; k < B.RowsCount; k++)
                    V._Matrix[i, k] = (int)(((long)A._Matrix[i, j] * B._Matrix[j, k] + V._Matrix[i, k]) % ProblemSolver.MOD);

        return V;
    }
    public static Matrix operator *(Matrix A, Matrix B)
    {
        if (A.ColumnsCount != B.RowsCount)
            throw new ArgumentException("");

        var res = new int[A.RowsCount, B.ColumnsCount];

        for (int i = 0; i < A.RowsCount; i++)
            for (int j = 0; j < B.ColumnsCount; j++)
                for (int k = 0; k < B.RowsCount; k++)
                    res[i, k] = (int)(((long)A._Matrix[i, j] * B._Matrix[j, k] + res[i, k]) % ProblemSolver.MOD);

        return new Matrix(A.RowsCount, B.ColumnsCount, res);
    }
}