using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().Select(c => c == '.' ? 1 : 0).ToArray();
		var n = s.Length;
		var k = int.Parse(Console.ReadLine());

		var cs = CumSum(s);

		var r = 0;
		foreach (var (i, j) in TwoPointers(n, n + 1, (i, j) => j == n || cs[j + 1] - cs[i] > k))
		{
			r = Math.Max(r, j - i);
		}
		return r;
	}

	public static int[] CumSum(int[] a)
	{
		var s = new int[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}

	static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
	{
		for (int i = 0, j = 0; i < n1 && j < n2; ++i)
			for (; j < n2; ++j)
				if (predicate(i, j)) { yield return (i, j); break; }
	}
}
