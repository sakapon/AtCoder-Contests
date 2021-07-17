using System;
using System.Collections.Generic;
using System.Linq;

class FL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, t) = Read2();
		var a = Read();

		var n2 = n / 2;

		var s1 = CreateSums(a.Take(n2).ToArray(), t);
		var s2 = CreateSums(a.Skip(n2).ToArray(), t);

		Array.Sort(s1);
		Array.Sort(s2);
		Array.Reverse(s2);

		Console.WriteLine(TwoPointers(s1, s2, (x, y) => x + y <= t).Max(_ => _.v1 + _.v2));
	}

	static long[] CreateSums(int[] a, int t)
	{
		var n = a.Length;
		var l = new List<long>();

		AllBoolCombination(n, b =>
		{
			var sum = 0L;
			for (int i = 0; i < n; ++i)
				if (b[i]) sum += a[i];
			if (sum <= t) l.Add(sum);
			return false;
		});
		return l.ToArray();
	}

	static void AllBoolCombination(int n, Func<bool[], bool> action)
	{
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;
		var b = new bool[n];

		for (int x = 0; x < pn; ++x)
		{
			for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;
			if (action(b)) break;
		}
	}

	static IEnumerable<(T1 v1, T2 v2)> TwoPointers<T1, T2>(T1[] a1, T2[] a2, Func<T1, T2, bool> predicate)
	{
		for (int i = 0, j = 0; i < a1.Length && j < a2.Length; ++i)
			for (; j < a2.Length; ++j)
				if (predicate(a1[i], a2[j])) { yield return (a1[i], a2[j]); break; }
	}
}
