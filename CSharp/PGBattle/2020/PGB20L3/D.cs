﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections.Statics.Typed;
using CoderLib8.Graphs.Int.Edges;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read3());

		var tree = new UndirectedEdgeGraph(n + 1);
		foreach (var (u, v, c) in es) tree.AddEdge(u, v, c);

		// 中間の辺
		UndirectedEdgeGraph.Edge me = null;
		Dfs2(new UndirectedEdgeGraph.Edge(-1, null, tree[1]));
		if (me == null) return 0;

		var v1 = me.From.Id;
		var v2 = me.To.Id;
		var d1 = new long[n + 1];
		var d2 = new long[n + 1];

		Array.Fill(d1, -1);
		Array.Fill(d2, -1);
		d1[v2] = 0;
		d2[v1] = 0;

		Dfs1(me.Reverse, d1);
		Dfs1(me, d2);

		var set1 = new ArrayItemSet<long>(d1.Where(x => x >= 0).OrderBy(x => x).ToArray());
		var set2 = new ArrayItemSet<long>(d2.Where(x => x >= 0).OrderBy(x => x).ToArray());

		var r = 0L;
		for (int v = 1; v <= n; v++)
		{
			if (d1[v] > 0)
			{
				var sd = d1[v] - d1[v1];
				var ed = d1[v] + d1[v1];
				r += set2.GetCount(sd + 1, ed + 1);
			}
			else
			{
				var sd = d2[v] - d2[v2];
				var ed = d2[v] + d2[v2];
				r += set1.GetCount(sd + 1, ed + 1);
			}
		}
		return r;

		void Dfs1(UndirectedEdgeGraph.Edge e, long[] d)
		{
			d[e.To.Id] = d[e.From.Id] + e.Cost;

			foreach (var ne in e.To.Edges)
			{
				if (ne.Id == e.Id) continue;
				Dfs1(ne, d);
			}
		}

		int Dfs2(UndirectedEdgeGraph.Edge e)
		{
			// 部分木の頂点の個数
			var vc = 1;
			foreach (var ne in e.To.Edges)
			{
				if (ne.Id == e.Id) continue;
				vc += Dfs2(ne);
			}
			if (vc == n >> 1) me = e;
			return vc;
		}
	}
}

namespace CoderLib8.Graphs.Int.Edges
{
	/// <summary>
	/// 無向グラフを表します。
	/// </summary>
	[System.Diagnostics.DebuggerDisplay(@"\{{VertexesCount} vertexes, {EdgesCount} edges\}")]
	public class UndirectedEdgeGraph
	{
		[System.Diagnostics.DebuggerDisplay(@"\{Vertex {Id} : {Edges.Count} edges\}")]
		public class Vertex
		{
			public int Id { get; }
			public List<Edge> Edges { get; } = new List<Edge>();

			public Vertex(int id)
			{
				Id = id;
			}
		}

		[System.Diagnostics.DebuggerDisplay(@"\{Edge {Id} : {From.Id} --> {To.Id}, Cost = {Cost}\}")]
		public class Edge
		{
			public int Id { get; }
			public Vertex From { get; }
			public Vertex To { get; }
			public long Cost { get; }
			public Edge Reverse { get; internal set; }

			public Edge(int id, Vertex from, Vertex to, long cost = 1)
			{
				Id = id;
				From = from;
				To = to;
				Cost = cost;
			}
		}

		public Vertex[] Vertexes { get; }
		public List<Edge> Edges { get; } = new List<Edge>();
		public int VertexesCount => Vertexes.Length;
		public int EdgesCount => Edges.Count;

		public Vertex this[int v] => Vertexes[v];

		public UndirectedEdgeGraph(int vertexesCount)
		{
			Vertexes = new Vertex[vertexesCount];
			for (int v = 0; v < vertexesCount; ++v) Vertexes[v] = new Vertex(v);
		}
		public UndirectedEdgeGraph(int vertexesCount, IEnumerable<(int u, int v)> edges) : this(vertexesCount)
		{
			foreach (var (u, v) in edges) AddEdge(u, v);
		}
		public UndirectedEdgeGraph(int vertexesCount, IEnumerable<(int u, int v, long cost)> edges) : this(vertexesCount)
		{
			foreach (var (u, v, cost) in edges) AddEdge(u, v, cost);
		}

		public void AddEdge(int u, int v, long cost = 1)
		{
			var fv = Vertexes[u];
			var tv = Vertexes[v];

			var e1 = new Edge(Edges.Count, fv, tv, cost);
			var e2 = new Edge(Edges.Count, tv, fv, cost);
			e1.Reverse = e2;
			e2.Reverse = e1;

			// 片側のみ登録します。
			Edges.Add(e1);
			fv.Edges.Add(e1);
			tv.Edges.Add(e2);
		}
	}
}

namespace CoderLib8.Collections.Statics.Typed
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayItemSet<T> : IEnumerable<T>
	{
		#region Initialization
		readonly int n;
		readonly T[] a;
		readonly T minItem, maxItem;
		readonly IComparer<T> comparer;

		// ソート済みであることを前提とします。
		public ArrayItemSet(T[] items, T minItem = default, T maxItem = default, IComparer<T> comparer = null)
		{
			a = items ?? throw new ArgumentNullException(nameof(items));
			n = a.Length;
			this.minItem = minItem;
			this.maxItem = maxItem;
			this.comparer = comparer ?? Comparer<T>.Default;
		}

		public int Count => n;
		public T[] Items => a;
		public T MinItem => minItem;
		public T MaxItem => maxItem;
		public IComparer<T> Comparer => comparer;
		#endregion

		#region By Index
		// 定義域 Z
		public T GetAt(int index) => index < 0 ? minItem : index >= n ? maxItem : a[index];
		#endregion

		#region By Item Range

		// 値域 (-1, n]
		public int GetFirstIndexGeq(T item)
		{
			int m, l = 0, r = n;
			while (l < r) if (comparer.Compare(a[m = l + r - 1 >> 1], item) >= 0) r = m; else l = m + 1;
			return r;
		}
		// 値域 [-1, n)
		public int GetLastIndexLeq(T item)
		{
			int m, l = -1, r = n - 1;
			while (l < r) if (comparer.Compare(a[m = l + r + 1 >> 1], item) <= 0) l = m; else r = m - 1;
			return l;
		}

		// secondary methods
		public T GetFirstGeq(T item) => GetAt(GetFirstIndexGeq(item));
		public T GetLastLeq(T item) => GetAt(GetLastIndexLeq(item));
		public int GetCountGeq(T item) => n - GetFirstIndexGeq(item);
		public int GetCountLeq(T item) => GetLastIndexLeq(item) + 1;
		public int GetCount(T startItem, T endItem) => GetFirstIndexGeq(endItem) - GetFirstIndexGeq(startItem);
		#endregion

		#region By Item
		public int GetFirstIndex(T item)
		{
			var i = GetFirstIndexGeq(item);
			return i < n && comparer.Compare(a[i], item) == 0 ? i : -1;
		}
		public int GetLastIndex(T item)
		{
			var i = GetLastIndexLeq(item);
			return i >= 0 && comparer.Compare(a[i], item) == 0 ? i : -1;
		}

		// secondary methods
		public bool Contains(T item) => GetFirstIndex(item) != -1;
		public int GetCount(T item) => GetLastIndexLeq(item) - GetFirstIndexGeq(item) + 1;
		#endregion

		#region Get Items
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)a).GetEnumerator();

		public IEnumerable<T> GetItems(T startItem, T endItem)
		{
			for (var i = GetFirstIndexGeq(startItem); i < n && comparer.Compare(a[i], endItem) < 0; ++i) yield return a[i];
		}

		public IEnumerable<T> GetItemsByIndex(int startIndex, int endIndex)
		{
			for (var i = startIndex; i < endIndex; ++i) yield return a[i];
		}
		#endregion
	}
}
