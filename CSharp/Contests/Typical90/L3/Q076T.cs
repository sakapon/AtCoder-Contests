using System;
using System.Collections.Generic;
using System.Linq;

class Q076T
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var sum = a.Sum();
		if (sum % 10 != 0) return false;
		sum /= 10;

		a = a.Concat(a).ToArray();
		var s = CumSumL(a);

		return TwoPointers(n, 2 * n, (i, j) => s[i] + sum <= s[j]).Any(_ => s[_.i] + sum == s[_.j]);
	}

	static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
	{
		for (int i = 0, j = 0; i < n1 && j < n2; ++i)
			for (; j < n2; ++j)
				if (predicate(i, j)) { yield return (i, j); break; }
	}

	static long[] CumSumL(long[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}
}
