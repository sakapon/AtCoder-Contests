using System;
using System.Collections.Generic;
using System.Linq;

class L2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps1 = Array.ConvertAll(new bool[n], _ => Read());
		var ps2 = Array.ConvertAll(new bool[m], _ => Read());

		var r = double.MaxValue;
		AllCombination(ps2, ps3 =>
		{
			var ps = ps1.Concat(ps3).ToArray();
			var nm = ps.Length;

			var es = new List<Edge>();
			for (int i = 0; i < nm; i++)
				for (int j = i + 1; j < nm; j++)
					es.Add(new Edge { i = i, j = j, cost = Norm(ps[i], ps[j]) });

			var mes = Prim(nm, 0, es.ToArray());
			r = Math.Min(r, mes.Sum(e => e.cost));

			return false;
		});

		return r;
	}

	struct Edge
	{
		public int i, j;
		public double cost;
	}

	static void AllCombination<T>(T[] values, Func<T[], bool> action)
	{
		var n = values.Length;
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;

		var rn = new int[n];
		for (int i = 0; i < n; ++i) rn[i] = i;

		for (int x = 0; x < pn; ++x)
		{
			var indexes = Array.FindAll(rn, i => (x & (1 << i)) != 0);
			if (action(Array.ConvertAll(indexes, i => values[i]))) break;
		}
	}

	static double Norm(int[] p, int[] q)
	{
		int dx = p[0] - q[0], dy = p[1] - q[1];
		return (p[2] == q[2] ? 1 : 10) * Math.Sqrt(dx * dx + dy * dy);
	}

	static Edge[] Prim(int n, int root, Edge[] ues) => Prim(n, root, ToMap(n, ues, false));
	static Edge[] Prim(int n, int root, List<Edge>[] map)
	{
		var u = new bool[n];
		var q = PQ<Edge>.Create(e => e.cost, map[root].ToArray());
		u[root] = true;
		var mes = new List<Edge>();

		// 実際の頂点数に注意。
		while (q.Count > 0 && mes.Count < n - 1)
		{
			var e = q.Pop();
			if (u[e.j]) continue;
			u[e.j] = true;
			mes.Add(e);
			foreach (var ne in map[e.j])
				if (ne.j != e.i)
					q.Push(ne);
		}
		return mes.ToArray();
	}

	static List<Edge>[] ToMap(int n, Edge[] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<Edge>());
		foreach (var e in es)
		{
			map[e.i].Add(e);
			if (!directed) map[e.j].Add(new Edge { i = e.j, j = e.i, cost = e.cost });
		}
		return map;
	}
}

class PQ<T>
{
	public static PQ<T> Create<TKey>(Func<T, TKey> getKey, T[] vs = null, bool desc = false)
	{
		var c = Comparer<TKey>.Default;
		return desc ?
			new PQ<T>(vs, (x, y) => c.Compare(getKey(y), getKey(x))) :
			new PQ<T>(vs, (x, y) => c.Compare(getKey(x), getKey(y)));
	}

	List<T> l = new List<T> { default(T) };
	Comparison<T> c;
	public T First => l[1];
	public int Count => l.Count - 1;
	public bool Any => l.Count > 1;

	PQ(T[] vs, Comparison<T> _c)
	{
		c = _c;
		if (vs != null) foreach (var v in vs) Push(v);
	}

	void Swap(int i, int j) { var o = l[i]; l[i] = l[j]; l[j] = o; }
	void UpHeap(int i) { for (int j; (j = i / 2) > 0 && c(l[j], l[i]) > 0; Swap(i, i = j)) ; }
	void DownHeap(int i)
	{
		for (int j; (j = 2 * i) < l.Count;)
		{
			if (j + 1 < l.Count && c(l[j], l[j + 1]) > 0) j++;
			if (c(l[i], l[j]) > 0) Swap(i, i = j); else break;
		}
	}

	public void Push(T v)
	{
		l.Add(v);
		UpHeap(Count);
	}
	public T Pop()
	{
		var r = l[1];
		l[1] = l[Count];
		l.RemoveAt(Count);
		DownHeap(1);
		return r;
	}
}
