using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	public struct Edge
	{
		public int Id, From, To;
		public Edge(int id, int from, int to) { Id = id; From = from; To = to; }
		public Edge GetReverse() => new Edge(Id, To, From);
	}

	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), s => int.Parse(s) - 1);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();
		var m = int.Parse(Console.ReadLine());
		var es = new bool[m].Select((_, i) => { var a = Read(); return new Edge(i + 1, a[0], a[1]); }).ToArray();

		var uf = new UF(n);
		var map = Array.ConvertAll(new bool[n], _ => new List<Edge>());
		var eCounts = new int[n];

		foreach (var e in es)
		{
			// Avoids cycle.
			if (!uf.Unite(e.From, e.To)) continue;

			map[e.From].Add(e);
			map[e.To].Add(e.GetReverse());
			eCounts[e.From]++;
			eCounts[e.To]++;
		}

		var gs = uf.ToGroups();
		foreach (var vs in gs)
		{
			var set = vs.ToHashSet();
			if (!set.SetEquals(vs.Select(i => p[i]))) return -1;
		}

		var r = new List<int>();
		var q = new Queue<int>();
		var u = new bool[n];

		foreach (var vs in gs)
		{
			foreach (var v in vs)
				if (eCounts[v] == 1) q.Enqueue(v);

			while (q.Count > 0)
			{
				var v0 = q.Dequeue();
				if (p[v0] != v0) Dfs(v0, -1);
				u[v0] = true;
				foreach (var e in map[v0])
				{
					if (u[e.To]) continue;
					if (--eCounts[e.To] == 1) q.Enqueue(e.To);
				}

				bool Dfs(int v, int pv)
				{
					foreach (var e in map[v])
					{
						if (e.To == pv) continue;
						if (u[e.To]) continue;

						if (p[e.To] == v0 || Dfs(e.To, v))
						{
							r.Add(e.Id);
							(p[e.From], p[e.To]) = (p[e.To], p[e.From]);
							return true;
						}
					}
					return false;
				}
			}
		}

		return $"{r.Count}\n" + string.Join(" ", r);
	}
}

class UF
{
	int[] p, sizes;
	public int GroupsCount;
	public UF(int n)
	{
		p = Enumerable.Range(0, n).ToArray();
		sizes = Array.ConvertAll(p, _ => 1);
		GroupsCount = n;
	}

	public int GetRoot(int x) => p[x] == x ? x : p[x] = GetRoot(p[x]);
	public int GetSize(int x) => sizes[GetRoot(x)];

	public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
	public bool Unite(int x, int y)
	{
		if ((x = GetRoot(x)) == (y = GetRoot(y))) return false;

		// 要素数が大きいほうのグループにマージします。
		if (sizes[x] < sizes[y]) Merge(y, x);
		else Merge(x, y);
		return true;
	}
	protected virtual void Merge(int x, int y)
	{
		p[y] = x;
		sizes[x] += sizes[y];
		--GroupsCount;
	}
	public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
}
