var t = Convert.ToInt32(Console.ReadLine());

for (int i = 0; i < t; i++)
{
    var trash = Console.ReadLine();
    var ladders = Console.ReadLine().Split().Select(inp => Convert.ToInt32(inp)).ToList();
    var testCase = new LaddersTestCase(ladders.Count);
    foreach (var ladder in ladders)
    {
        testCase.AddLadder(ladder);
    }

    var stepsHeights = Console.ReadLine().Split().Select(inp => Convert.ToInt32(inp)).ToList();
    foreach (var step in stepsHeights)
    {
        Console.Write(testCase.GetResult(step));
        Console.Write(' ');
    }
    Console.WriteLine();
}
public class LaddersTestCase
{
    private long[] _totalHeight;
    private int _laddersAmount;
    private int _addedLaddersAmount = 0;
    private LadderDescription[] _data;
    private bool _isInit = false;
    private static LadderDescriptionComparer _comparer = new LadderDescriptionComparer();
    public LaddersTestCase(int laddersAmount)
    {
        _laddersAmount = laddersAmount;
        _totalHeight = new long[laddersAmount];
        _data = new LadderDescription[laddersAmount];
    }

    private void Init()
    {
        Array.Sort(_data, _comparer);

        var min = int.MaxValue;
        for (int i = _data.Length-1; i >= 0; i--)
        {
            min = Math.Min(_data[i].Order, min);
            _data[i].Order = min;
        }
        _isInit = true;
    }

    public long GetResult(int stepHeight)
    {
        if (!_isInit)
            Init();

        var l = _data.GetRighttBound(new LadderDescription { Height = stepHeight, Order = int.MaxValue}, _comparer);
        var firstUnscorableLadderPosition = l == _data.Length ? _data.Length - 1 : _data[l].Order - 1;
        return firstUnscorableLadderPosition == -1 ? 0 :  _totalHeight[firstUnscorableLadderPosition];
    }
    public void AddLadder(int ladderHeight)
    {
        if (_addedLaddersAmount >= _laddersAmount)
            throw new Exception("exceded ladders limit");

        _totalHeight[_addedLaddersAmount] = _addedLaddersAmount == 0
            ? ladderHeight
            : ladderHeight + _totalHeight[_addedLaddersAmount - 1];

        _data[_addedLaddersAmount] = new LadderDescription
        {
            Height = ladderHeight,
            Order = _addedLaddersAmount,
        };

        _addedLaddersAmount++;
    }

    private class LadderDescriptionComparer : IComparer<LadderDescription>
    {
        public int Compare(LadderDescription? x, LadderDescription? y)
        {
            if (x.Height > y.Height)
                return 1;
            if (x.Height < y.Height)
                return -1;
            if (x.Order > y.Order)
                return 1;
            if (x.Order < y.Order)
                return -1;

            return 0;
        }
    }

    private class LadderDescription
    {
        public int Height { get; set; }
        public int Order { get; set; }
    }
}

public static class BinarrySearchHelper
{
    public static int GetRighttBound<T>(this T[] collection, T value, IComparer<T> comparator)
    {

        int l = 0;
        int r = collection.Length -1;

        while(l < r)
        {
            int m = (l + r) / 2;
            if (comparator.Compare(collection[m], value) <= 0) l = m + 1;
            else r = m - 1;
        }

        if (comparator.Compare(collection[l], value) <= 0) l++;

        return l;
    }
}