using System;
using System.Collections.Generic;

class I3
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		// 3^30
		const long max = 205891132094649;
		var l = new List<long>();
		var c = 0;

		foreach (var v in a)
		{
			var x = v;
			while ((x & 1) == 0)
			{
				x >>= 1;
				c++;
			}

			while (x < max)
			{
				l.Add(x);
				x *= 3;
			}
		}

		a = l.ToArray();
		Array.Sort(a);
		return a[c];
	}
}
