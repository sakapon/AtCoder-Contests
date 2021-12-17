using System;
using System.Collections.Generic;
using System.Linq;

class C3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var b = Read().Append(-1 << 30).Append(int.MaxValue).ToArray();

		Array.Sort(a);
		Array.Sort(b);
		return TwoPointers(n, m + 2, (i, j) => a[i] <= b[j]).Min(_ => Math.Min(b[_.j] - a[_.i], a[_.i] - b[_.j - 1]));
	}

	static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
	{
		for (int i = 0, j = 0; i < n1 && j < n2; ++i)
			for (; j < n2; ++j)
				if (predicate(i, j)) { yield return (i, j); break; }
	}
}
