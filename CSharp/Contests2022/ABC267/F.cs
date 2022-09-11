using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		var qc = Read()[0];
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var (mv1, mv2, md) = (-1, -1, -1);
		var tree0 = new Tree(n + 1, 1, es);
		for (int v = 1; v <= n; v++)
		{
			var d = tree0.Depths[v];
			if (md < d) (mv1, md) = (v, d);
		}

		md = -1;
		var tree1 = new Tree(n, mv1, tree0.Map);
		for (int v = 1; v <= n; v++)
		{
			var d = tree1.Depths[v];
			if (md < d) (mv2, md) = (v, d);
		}

		var qmap = Array.ConvertAll(new bool[n + 1], _ => new List<(int, int)>());
		for (int qi = 0; qi < qc; qi++)
		{
			var q = qs[qi];
			qmap[q[0]].Add((qi, q[1]));
		}

		var r = new int[qc];
		Array.Fill(r, -1);
		var path = new ArrayStack<int>();

		Dfs(mv1, -1);
		Dfs(mv2, -1);
		return string.Join("\n", r);

		void Dfs(int v, int pv)
		{
			path.Add(v);

			foreach (var (qi, k) in qmap[v])
			{
				if (path.Count <= k) continue;
				r[qi] = path[k];
			}

			foreach (var nv in tree0.Map[v])
			{
				if (nv == pv) continue;
				Dfs(nv, v);
			}
			path.Pop();
		}
	}
}

public class Tree
{
	static List<int>[] ToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}

	public int Count { get; }
	public int Root { get; }
	public List<int>[] Map { get; }
	public int[] Depths { get; }
	public int[] Parents { get; }

	// この Euler Tour では方向を記録しません。
	// order -> vertex
	public int[] Tour { get; }
	// vertex -> orders
	public List<int>[] TourMap { get; }

	public Tree(int n, int root, int[][] ues) : this(n, root, ToMap(n, ues, false)) { }
	public Tree(int n, int root, List<int>[] map)
	{
		Count = n;
		Root = root;
		Map = map;
		Depths = Array.ConvertAll(Map, _ => -1);
		Parents = Array.ConvertAll(Map, _ => -1);

		var tour = new List<int>();
		TourMap = Array.ConvertAll(Map, _ => new List<int>());

		Depths[root] = 0;
		Dfs(root, -1);

		Tour = tour.ToArray();

		void Dfs(int v, int pv)
		{
			TourMap[v].Add(tour.Count);
			tour.Add(v);

			foreach (var nv in Map[v])
			{
				if (nv == pv) continue;
				Depths[nv] = Depths[v] + 1;
				Parents[nv] = v;
				Dfs(nv, v);

				TourMap[v].Add(tour.Count);
				tour.Add(v);
			}
		}
	}
}

[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
public class ArrayStack<T> : IEnumerable<T>
{
	T[] a;
	int n;

	public ArrayStack(int capacity = 2)
	{
		var c = 1;
		while (c < capacity) c <<= 1;
		a = new T[c];
	}

	public int Count => n;
	public T this[int i]
	{
		get => a[n - 1 - i];
		set => a[n - 1 - i] = value;
	}
	public T First
	{
		get => a[n - 1];
		set => a[n - 1] = value;
	}

	public void Clear() => n = 0;
	public void Add(T item)
	{
		if (n == a.Length) Expand();
		a[n++] = item;
	}
	public T Pop() => a[--n];

	void Expand()
	{
		Array.Resize(ref a, a.Length << 1);
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
	public IEnumerator<T> GetEnumerator() { for (var i = n - 1; i >= 0; --i) yield return a[i]; }

	public T[] ToArray()
	{
		var r = new T[n];
		Array.Copy(a, r, n);
		Array.Reverse(r);
		return r;
	}
}
