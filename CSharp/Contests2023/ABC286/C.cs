using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, a, b) = Read3L();
		var s = Console.ReadLine();

		var r = 1L << 60;

		// A を ka 回
		for (int ka = 0; ka < n; ka++)
		{
			var kb = 0;
			for (int i = 0; i < n >> 1; i++)
			{
				if (s[i] != s[(int)n - 1 - i]) kb++;
			}
			ChFirstMin(ref r, ka * a + kb * b);

			s = s[1..] + s[0];
		}
		return r;
	}

	public static void ChFirstMin<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) > 0) o1 = o2; }
}
