using System;
using System.Collections.Generic;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var a = Read();

		if (n == k) return $"0 {k}";

		Array.Sort(a);
		Array.Reverse(a);

		var next = new int[n + 1];
		foreach (var (i, j) in TwoPointers(n + 1, n + 1, (i, j) => j == n || a[i] >= a[j] * 2))
		{
			next[i] = j;
		}

		var jump = (int[])next.Clone();

		for (int mt = 1; ; mt++)
		{
			var ma = 1 << 30;

			for (int i = 0; i < n; i++)
			{
				var ca = n - (jump[i] - i);
				ma = Math.Min(ma, ca);
			}
			if (ma <= k) return $"{mt} {ma}";

			for (int i = 0; i < n; i++)
			{
				jump[i] = next[jump[i]];
			}
		}
	}

	static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
	{
		for (int i = 0, j = 0; i < n1 && j < n2; ++i)
			for (; j < n2; ++j)
				if (predicate(i, j)) { yield return (i, j); break; }
	}
}
