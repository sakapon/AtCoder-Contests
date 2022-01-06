using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b) = Read2();
		a--;
		b--;
		var s = Array.ConvertAll(new bool[3], _ => Console.ReadLine());

		var nexts = Enumerable.Range(0, 9).Select(id => (i: id / 3 - 1, j: id % 3 - 1)).ToArray();
		nexts = Array.FindAll(nexts, p => s[p.i + 1][p.j + 1] == '#');

		return Dfs(p =>
		{
			var (i, j) = p;
			return nexts
				.Select(d => (i: i + d.i, j: j + d.j))
				.Where(q => 0 <= q.i && q.i < 9)
				.Where(q => 0 <= q.j && q.j < 9)
				.ToArray();
		}, (a, b), (-1, -1)).Count;
	}

	public static HashSet<TVertex> Dfs<TVertex>(Func<TVertex, TVertex[]> nexts, TVertex sv, TVertex ev)
	{
		var eq = EqualityComparer<TVertex>.Default;
		var u = new HashSet<TVertex>();
		var q = new Stack<TVertex>();
		u.Add(sv);
		q.Push(sv);

		while (q.Count > 0)
		{
			var v = q.Pop();

			foreach (var nv in nexts(v))
			{
				if (u.Contains(nv)) continue;
				u.Add(nv);
				if (eq.Equals(nv, ev)) return u;
				q.Push(nv);
			}
		}
		return u;
	}
}
