using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var c = ReadL();

		var q = new Queue<int>(Enumerable.Range(1, (1 << n) - 1).OrderBy(x => c[x - 1]));
		var a = new List<int>();
		var es = new List<int>();

		while (es.Count < n)
		{
			var x0 = q.Dequeue();
			var x = x0;

			foreach (var e in es)
			{
				var f = GetFirstBit(e);
				if ((x & f) != 0) x ^= e;
			}

			if (x == 0) continue;
			var g = GetFirstBit(x);

			for (int i = 0; i < es.Count; i++)
			{
				if ((es[i] & g) != 0) es[i] ^= x;
			}

			a.Add(x0);
			es.Add(x);
		}

		return a.Sum(x => c[x - 1]);
	}

	static int GetFirstBit(int x)
	{
		for (int f = 1; ; f <<= 1)
			if ((x & f) != 0) return f;
	}
}
