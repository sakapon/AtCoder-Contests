using System;
using System.Collections.Generic;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		foreach (var (l, r) in es)
		{
			map[l - 1].Add(r);
			map[r].Add(l - 1);
		}
		return Dfs(n + 1, v => map[v].ToArray(), 0, n)[n];
	}

	public static bool[] Dfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var u = new bool[n];
		var q = new Stack<int>();
		u[sv] = true;
		q.Push(sv);

		while (q.Count > 0)
		{
			var v = q.Pop();

			foreach (var nv in nexts(v))
			{
				if (u[nv]) continue;
				u[nv] = true;
				if (nv == ev) return u;
				q.Push(nv);
			}
		}
		return u;
	}
}
