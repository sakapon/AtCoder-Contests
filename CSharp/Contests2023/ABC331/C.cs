using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var sorted = a.Select((v, i) => (v, i)).OrderBy(p => -p.v).ToArray();

		var s = new long[n + 1];
		for (int i = 0; i < n; i++)
		{
			s[i + 1] = s[i] + sorted[i].v;
		}
		for (int i = 1; i < n; i++)
		{
			if (sorted[i - 1].v == sorted[i].v)
			{
				s[i] = s[i - 1];
			}
		}

		var r = new long[n];
		for (int i = 0; i < n; i++)
		{
			r[sorted[i].i] = s[i];
		}

		return string.Join(" ", r);
	}
}
