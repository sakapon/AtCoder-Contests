using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, t) = Read2();
		var a = ReadL();

		var n2 = n / 2;

		var s1 = CreateSums(a.Take(n2).ToArray(), t);
		var s2 = CreateSums(a.Skip(n2).ToArray(), t);

		Array.Sort(s1);
		Array.Sort(s2);
		Array.Reverse(s2);

		var r = 0L;
		for (int i = 0, j = 0; i < s1.Length; i++)
		{
			while (s1[i] + s2[j] > t) j++;
			r = Math.Max(r, s1[i] + s2[j]);
		}
		Console.WriteLine(r);
	}

	static long[] CreateSums(long[] a, int t)
	{
		var l = new List<long>();

		AllCombination(a, p =>
		{
			var sum = p.Sum();
			if (sum <= t) l.Add(sum);
			return false;
		});
		return l.ToArray();
	}

	static void AllCombination<T>(T[] values, Func<T[], bool> action)
	{
		var n = values.Length;
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;

		var rn = new int[n];
		for (int i = 0; i < n; ++i) rn[i] = i;

		for (int x = 0; x < pn; ++x)
		{
			var indexes = Array.FindAll(rn, i => (x & (1 << i)) != 0);
			if (action(Array.ConvertAll(indexes, i => values[i]))) break;
		}
	}
}
