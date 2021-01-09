using System;
using System.Collections.Generic;
using System.Linq;

class TreeLca
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], qc = h[1];
		var p = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var d = p.Select((v, i) => (p: v, c: i + 1)).ToLookup(x => x.p, x => x.c);
		var map = Enumerable.Range(0, n).Select(i => d[i].ToList()).ToArray();

		var lca = new Lca(n, 0, map);
		Console.WriteLine(string.Join("\n", qs.Select(q => lca.GetLca(q[0], q[1]))));
	}
}

// n: 個数
class Lca
{
	// IList<IList<int>> などでは TLE。
	List<int>[] map;
	List<int> tour;
	List<int>[] ordersMap;
	List<int> depths;
	ST1<int> depthST;

	public Lca(int n, int root, List<int>[] _map)
	{
		map = _map;
		tour = new List<int>();
		ordersMap = Array.ConvertAll(new int[n], _ => new List<int>());
		depths = new List<int>();
		EulerTourDfs(root, 0);
		depthST = new ST1<int>(2 * n, Math.Min, int.MaxValue, depths.ToArray());
	}

	// 頂点と辺で 1 セット。
	void EulerTourDfs(int v, int depth)
	{
		// Vertex
		ordersMap[v].Add(tour.Count);
		depths.Add(depth);
		foreach (var nv in map[v])
		{
			// Edge
			tour.Add(v);
			EulerTourDfs(nv, depth + 1);
			tour.Add(-nv);

			// Vertex
			ordersMap[v].Add(tour.Count);
			depths.Add(depth);
		}
	}

	public int GetLca(int u, int v)
	{
		if (u == v) return u;
		if (ordersMap[u][0] > ordersMap[v][0]) { var t = u; u = v; v = t; }
		if (ordersMap[u].Last() > ordersMap[v][0]) return u;

		var minDepth = depthST.Get(ordersMap[u].Last(), ordersMap[v][0]);
		var lcaOrder = depthST.Aggregate(ordersMap[u].Last(), ordersMap[v][0], -1, (p, n, l) =>
		{
			if (p != -1 || depthST[n] != minDepth) return p;
			while (l > 1)
			{
				n = depthST[n.Child0] == minDepth ? n.Child0 : n.Child1;
				l >>= 1;
			}
			return depthST.Original(n);
		});
		return tour[lcaOrder];
	}
}

class ST1<TV>
{
	public struct STNode
	{
		public int i;
		public static implicit operator STNode(int i) => new STNode { i = i };
		public override string ToString() => $"{i}";

		public STNode Parent => i >> 1;
		public STNode Child0 => i << 1;
		public STNode Child1 => (i << 1) + 1;
		public STNode LastLeft(int length) => i * length;
		public STNode LastRight(int length) => (i + 1) * length;
	}

	// Power of 2
	public int n2 = 1;
	public TV[] a2;

	public Func<TV, TV, TV> Union;
	public TV v0;

	// 全ノードを、零元を表す値で初期化します (零元の Union もまた零元)。
	public ST1(int n, Func<TV, TV, TV> union, TV _v0, TV[] a0 = null)
	{
		while (n2 < n << 1) n2 <<= 1;
		a2 = new TV[n2];

		Union = union;
		v0 = _v0;
		if (!Equals(v0, default(TV)) || a0 != null) Init(a0);
	}

	public void Init(TV[] a0 = null)
	{
		if (a0 == null)
		{
			for (int i = 1; i < n2; ++i) a2[i] = v0;
		}
		else
		{
			Array.Copy(a0, 0, a2, n2 >> 1, a0.Length);
			for (int i = (n2 >> 1) + a0.Length; i < n2; ++i) a2[i] = v0;
			for (int i = (n2 >> 1) - 1; i > 0; --i) a2[i] = Union(a2[i << 1], a2[(i << 1) + 1]);
		}
	}

	public STNode Actual(int i) => (n2 >> 1) + i;
	public int Original(STNode n) => n.i - (n2 >> 1);
	public TV this[STNode n]
	{
		get { return a2[n.i]; }
		set { a2[n.i] = value; }
	}

	// Bottom-up
	public void Set(int i, TV v)
	{
		var n = Actual(i);
		a2[n.i] = v;
		while ((n = n.Parent).i > 0) a2[n.i] = Union(a2[n.Child0.i], a2[n.Child1.i]);
	}

	public TV Get(int i) => a2[(n2 >> 1) + i];
	// 範囲の昇順
	public TV Get(int l_in, int r_ex)
	{
		int l = (n2 >> 1) + l_in, r = (n2 >> 1) + r_ex;

		var v = v0;
		while (l < r)
		{
			var length = l & -l;
			while (l + length > r) length >>= 1;
			v = Union(v, a2[l / length]);
			l += length;
		}
		return v;
	}

	// 範囲の昇順
	// (previous, node, length) => result
	public TR Aggregate<TR>(int l_in, int r_ex, TR r0, Func<TR, STNode, int, TR> func)
	{
		int al = (n2 >> 1) + l_in, ar = (n2 >> 1) + r_ex;

		var rv = r0;
		while (al < ar)
		{
			var length = al & -al;
			while (al + length > ar) length >>= 1;
			rv = func(rv, al / length, length);
			al += length;
		}
		return rv;
	}
}
