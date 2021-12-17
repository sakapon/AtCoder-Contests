using System;
using System.Linq;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().Select(c => c == '.' ? 1 : 0).ToArray();
		var n = s.Length;
		var k = int.Parse(Console.ReadLine());

		var cs = CumSum(s);
		return Last(0, n, x => Enumerable.Range(0, n - x + 1).Any(i => cs[i + x] - cs[i] <= k));
	}

	public static int[] CumSum(int[] a)
	{
		var s = new int[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
