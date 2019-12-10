using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	struct R
	{
		public int P, C, D;
	}

	struct P
	{
		public int Id, Order, Sign, Depth, Distance, ColorDistance, ColorCount;
	}

	class Context
	{
		public int[] P2 = Enumerable.Range(0, 20).Select(i => (int)Math.Pow(2, i)).ToArray();
		public int Distance;
		public List<int> Stack = new List<int>();
		public List<R>[] Map;
		public List<int>[] Parents;

		public int Order;
		public P[] Points;
		public int[] ColorDistances;
		public int[] ColorCounts;
		public List<P>[] ColorTours;
	}

	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var n = h[0];

		var c = new Context
		{
			Parents = new int[n + 1].Select(_ => new List<int>()).ToArray(),
			Points = new P[n + 1],
			ColorDistances = new int[n],
			ColorCounts = new int[n],
			ColorTours = new int[n].Select(_ => new List<P>()).ToArray(),
			Map = new int[n + 1].Select(_ => new List<R>()).ToArray(),
		};
		foreach (var r in new int[n - 1].Select(_ => read()))
		{
			c.Map[r[0]].Add(new R { P = r[1], C = r[2], D = r[3] });
			c.Map[r[1]].Add(new R { P = r[0], C = r[2], D = r[3] });
		}

		c.Map[0].Add(new R { P = 1 });
		Dfs(0, 0, c);

		Console.WriteLine(string.Join("\n", new int[h[1]].Select(_ => Answer(read(), c))));
	}

	static void Dfs(int p, int parent, Context c)
	{
		foreach (var r in c.Map[p])
		{
			if (r.P == parent) continue;

			for (int i = 0; c.P2[i] <= c.Stack.Count; i++)
				c.Parents[r.P].Add(c.Stack[c.Stack.Count - c.P2[i]]);

			c.Stack.Add(r.P);
			c.Distance += r.D;
			c.ColorDistances[r.C] += r.D;
			c.ColorCounts[r.C]++;

			var pd = new P
			{
				Id = r.P,
				Order = ++c.Order,
				Sign = 1,
				Depth = c.Stack.Count,
				Distance = c.Distance,
				ColorDistance = c.ColorDistances[r.C],
				ColorCount = c.ColorCounts[r.C],
			};
			c.ColorTours[r.C].Add(pd);
			c.Points[r.P] = pd;

			Dfs(r.P, p, c);

			c.Stack.RemoveAt(c.Stack.Count - 1);
			c.Distance -= r.D;
			c.ColorDistances[r.C] -= r.D;
			c.ColorCounts[r.C]--;

			var pu = new P
			{
				Id = r.P,
				Order = ++c.Order,
				Sign = -1,
				Depth = c.Stack.Count,
				Distance = c.Distance,
				ColorDistance = c.ColorDistances[r.C],
				ColorCount = c.ColorCounts[r.C],
			};
			c.ColorTours[r.C].Add(pu);
		}
	}

	static int Answer(int[] q, Context c)
	{
		var p1 = c.Points[q[2]];
		var p2 = c.Points[q[3]];

		var r1 = p1.Depth < p2.Depth ? p1 : p2;
		var r2 = p1.Depth < p2.Depth ? p2 : p1;

		while (r1.Depth < r2.Depth)
			r2 = c.Points[c.Parents[r2.Id][(int)Math.Log(r2.Depth - r1.Depth, 2)]];
		while (r1.Id != r2.Id)
		{
			var k = 0;
			while (k < c.Parents[r1.Id].Count && c.Parents[r1.Id][k] != c.Parents[r2.Id][k]) k++;
			if (k > 0) k--;
			r1 = c.Points[c.Parents[r1.Id][k]];
			r2 = c.Points[c.Parents[r2.Id][k]];
		}

		return Replace(p1, q[0], q[1], c) + Replace(p2, q[0], q[1], c) - 2 * Replace(r1, q[0], q[1], c);
	}

	static int Replace(P p, int x, int y, Context c)
	{
		var k = SearchLast(i => c.ColorTours[x][i].Order <= p.Order, -1, c.ColorTours[x].Count - 1);
		return p.Distance - (k == -1 ? 0 : c.ColorTours[x][k].ColorDistance) + (k == -1 ? 0 : c.ColorTours[x][k].ColorCount) * y;
	}

	static int SearchLast(Func<int, bool> f, int l, int r)
	{
		int m;
		while (l < r) if (f(m = (l + r + 1) / 2)) l = m; else r = m - 1;
		return r;
	}
}
