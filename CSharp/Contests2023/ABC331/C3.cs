using System;
using System.Collections.Generic;
using System.Linq;

class C3
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var b = (long[])a.Clone();

		Array.Sort(b);
		Array.Reverse(b);

		var s = new long[n + 1];
		for (int i = 0; i < n; i++)
		{
			s[i + 1] = s[i] + b[i];
		}
		for (int i = 1; i < n; i++)
		{
			if (b[i - 1] == b[i])
			{
				s[i] = s[i - 1];
			}
		}

		var r = new long[1000001];
		for (int i = 0; i < n; i++)
		{
			r[b[i]] = s[i];
		}
		return string.Join(" ", a.Select(v => r[v]));
	}
}
