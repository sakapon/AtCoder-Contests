using System;
using System.Collections.Generic;

class Q006S
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var s = Console.ReadLine();

		var r = new List<char>();

		var set = new SortedSet<long>();
		for (int i = 0; i < n - k; i++)
		{
			set.Add(s[i] * 100000L + i);
		}

		long t = -1, ti;
		for (int i = n - k; i < n; i++)
		{
			set.Add(s[i] * 100000L + i);
			while ((ti = set.Min % 100000L) < t) set.Remove(set.Min);

			set.Remove(set.Min);
			t = ti;
			r.Add(s[(int)t]);
		}

		return string.Join("", r);
	}
}
