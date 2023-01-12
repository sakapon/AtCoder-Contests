using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var s = ReadL();

		var r = 0L;
		var set = new HashSet<long>();
		var ts = 0L;

		for (int i = 0, j = 0; i < n; i++)
		{
			while (j < n && !set.Contains(a[j]))
			{
				set.Add(a[j]);
				ts += s[j - i];
				j++;
			}

			r += ts;
			r %= M;
			set.Remove(a[i]);
			ts -= s[j - i - 1];
		}
		return r;
	}

	const long M = 998244353;
}
