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
		var a = Array.ConvertAll(new bool[2 * n - 1], _ => Read());

		var r = 0;
		Dfs(0, Enumerable.Range(0, 2 * n).ToList());
		return r;

		void Dfs(int xor, List<int> l)
		{
			if (l.Count == 0)
			{
				r = Math.Max(r, xor);
				return;
			}

			var i = l[0];
			l.RemoveAt(0);

			for (int ji = 0; ji < l.Count; ji++)
			{
				var j = l[ji];
				l.RemoveAt(ji);
				Dfs(xor ^ a[i][j - i - 1], l);
				l.Insert(ji, j);
			}

			l.Insert(0, i);
		}
	}
}
