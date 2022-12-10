using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		return Dfs(1 << 29, a);

		int Dfs(int f, int[] a)
		{
			if (f == 0) return 0;

			var l0 = new List<int>();
			var l1 = new List<int>();

			foreach (var v in a)
			{
				if ((v & f) == 0)
				{
					l0.Add(v);
				}
				else
				{
					l1.Add(v - f);
				}
			}

			if (l0.Count == 0)
			{
				return Dfs(f >> 1, l1.ToArray());
			}
			else if (l1.Count == 0)
			{
				return Dfs(f >> 1, l0.ToArray());
			}
			else
			{
				var r0 = Dfs(f >> 1, l0.ToArray());
				var r1 = Dfs(f >> 1, l1.ToArray());
				return f | Math.Min(r0, r1);
			}
		}
	}
}
