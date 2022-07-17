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
		var (a, n) = Read2();

		var max = 1000000;
		var u = new bool[max];
		var set = new HashSet<long> { 1 };
		var t = new HashSet<long>();

		for (int k = 1; ; k++)
		{
			foreach (var x in set)
			{
				var nx = x * a;
				if (nx < max && !u[nx])
				{
					u[nx] = true;
					t.Add(nx);
				}

				if (x >= 10 && x % 10 != 0)
				{
					var s = x.ToString();
					s = s[^1] + s[..^1];
					nx = int.Parse(s);
					if (nx < max && !u[nx])
					{
						u[nx] = true;
						t.Add(nx);
					}
				}
			}

			if (t.Count == 0) return -1;
			if (t.Contains(n)) return k;
			(set, t) = (t, set);
			t.Clear();
		}
	}
}
