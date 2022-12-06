var ReadInt = () => Convert.ToInt32(Console.ReadLine());
var ReadInts = () => Console.ReadLine().Split().Select(inp => Convert.ToInt32(inp)).ToList();
var main = () =>
{
    var problemSolver = new ProblemSolver();
    problemSolver.Solve(ReadInt());
};

main();

public class InteractHelper{
    public bool ReadRangeResult() => 
     Console.ReadLine() == "YES";

    public bool AskRangeQuestionAndGetResult(int l, int r)
    {
        var length = r - l + 1;

        Console.Write($"? {length} ");
        for (var i = l; i <= r; i++)
        {
            Console.Write($"{i} ");
        }
        Console.WriteLine();
        Console.Out.Flush();

        return ReadRangeResult();
    }

    public bool TryMakeJudjment(int ans)
    {
        Console.WriteLine($"! {ans} ");

        return (Console.ReadLine().Trim() == ":)");
    }
}

public class ProblemSolver
{

    InteractHelper _interactHelper = new InteractHelper();

    int _answerAttempts = 0;
    private bool TryGessAnswer(int answer)
    {
        _answerAttempts++;
        return _interactHelper.TryMakeJudjment(answer);
    }

    private bool AskRangeQuestionAndGetResult(int l, int r)
    {
        return _interactHelper.AskRangeQuestionAndGetResult(l, r);
    }

    HashSet<string> _excluded = new HashSet<string>();
    private void ExcludeRange(int l, int r)
    {
        _excluded.Add($"{l}_{r}");
    }

    HashSet<int> _judjings = new HashSet<int>();
    private void MakeUnderJudjment(int p)
    {
        _judjings.Add(p);
    }

    private bool IsRangeIncluded(int l, int r)
    {
        return !_excluded.Contains($"{l}_{r}");
    }

    bool _end = false;
    private bool MakeJudjment(int l, int r, bool previous = true, bool granted = false)
    {
        if (_end)
            return true;

        if (!IsRangeIncluded(l, r))
            return previous;

        int m = l + 1 < r ? (l + r) / 2 : l + 1;

        var resL1 = AskRangeQuestionAndGetResult(l, m - 1);

        if (l == r)
        {
            ExcludeRange(l, r);
            if (!previous && granted && resL1)
            {
                TryGessAnswer(l);
                _end = true;
            }

            if (granted)
            {
                if(TryGessAnswer(l))
                {
                    _end = true;
                    return true;
                }

                return false;
            }
            //if (!previous && !granted && resL1)
            //{
            //    MakeUnderJudjment(l);
            //}
            MakeUnderJudjment(l);
            return true;
        }

        if (!previous)
        {
            return resL1 ? MakeJudjment(l, m - 1)
                : MakeJudjment(m, r);
        }

        var resR1 = AskRangeQuestionAndGetResult(m, r);

        //case 1
        if (resL1 != resR1)
        {
            //case 1y => l
            if (resL1)
            {
                ExcludeRange(m, r);
                return MakeJudjment(l, m - 1, true, true);
            }
            else
            {
                ExcludeRange(l, m - 1);
                return MakeJudjment(m, r, true, true);
            }
        }

        var resR2 = AskRangeQuestionAndGetResult(m, r);

        //case 2a
        if (resR2 == resR1)
        {
            //case 2ay => l
            if (!resL1)
            {
                ExcludeRange(m, r);
                return MakeJudjment(l, m - 1, true, true);
            }
            else
            {
                ExcludeRange(l, m - 1);
                return MakeJudjment(m, r, true, true);
            }
        }

        //case 2b

        if (resR2)
        {
            previous = MakeJudjment(l, m - 1, false, false);
            if (_end)
                return true;
            return MakeJudjment(m, r, previous, false);
        }
        else
        {
            previous = MakeJudjment(m, r, false, false);
            if (_end)
                return true;
            return MakeJudjment(r, m - 1, previous, false);
        }
    }
    public void Solve(int x)
    {
        MakeJudjment(1, x);

        foreach (var i in _judjings)
            if(TryGessAnswer(i))
                break;
    }
}

