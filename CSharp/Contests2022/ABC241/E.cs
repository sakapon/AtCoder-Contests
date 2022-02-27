using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2L();
		var a = ReadL();

		var u = new int[n];
		Array.Fill(u, -1);
		var path = new List<int>();
		var x = 0L;
		var m = 0;

		for (int i = 0; u[m] == -1; i++)
		{
			u[m] = i;
			path.Add(m);

			x += a[m];
			m = (int)(x % n);
		}

		if (k <= path.Count)
		{
			return Enumerable.Range(0, (int)k).Sum(i => a[path[i]]);
		}

		k -= path.Count;

		var loopLength = path.Count - u[m];
		var loopSum = path.Skip(u[m]).Sum(v => a[v]);
		var loops = k / loopLength;

		x += loops * loopSum;
		k -= loops * loopLength;

		x += Enumerable.Range(u[m], (int)k).Sum(i => a[path[i]]);
		return x;
	}
}
