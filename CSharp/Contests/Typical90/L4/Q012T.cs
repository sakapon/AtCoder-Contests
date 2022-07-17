using System;
using System.Collections.Generic;

class Q012T
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var qc = Read()[0];
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		(int, int)[] Nexts((int i, int j) p) => new[] { (p.i, p.j + 1), (p.i, p.j - 1), (p.i + 1, p.j), (p.i - 1, p.j) };
		var uf = new UnionFind<(int, int)>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				var p = (q[1], q[2]);
				uf.Add(p);

				foreach (var np in Nexts(p))
					if (uf.GetRoot(np) != null) uf.Unite(p, np);
			}
			else
			{
				var a = (q[1], q[2]);
				var b = (q[3], q[4]);
				Console.WriteLine(uf.GetRoot(a) != null && uf.AreUnited(a, b) ? "Yes" : "No");
			}
		}
		Console.Out.Flush();
	}
}

[System.Diagnostics.DebuggerDisplay(@"Count = {Count}, GroupsCount = {GroupsCount}")]
public class UnionFind<T>
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
	public class Node
	{
		public T Item;
		public Node Parent;
		public int Size = 1;
	}

	Dictionary<T, Node> nodes = new Dictionary<T, Node>();
	public int Count => nodes.Count;
	public int GroupsCount { get; private set; }

	public UnionFind(IEnumerable<T> collection = null)
	{
		if (collection != null)
			foreach (var x in collection) nodes[x] = new Node { Item = x };
		GroupsCount = nodes.Count;
	}

	public Node Add(T x)
	{
		if (nodes.ContainsKey(x)) return null;
		++GroupsCount;
		return nodes[x] = new Node { Item = x };
	}

	public Node GetRoot(T x) => nodes.ContainsKey(x) ? GetRoot(nodes[x]) : null;
	Node GetRoot(Node n) => n.Parent == null ? n : n.Parent = GetRoot(n.Parent);

	public bool AreUnited(T x, T y)
	{
		if (nodes.Comparer.Equals(x, y)) return true;
		var nx = GetRoot(x);
		var ny = GetRoot(y);
		return nx != null && nx == ny;
	}
	public int? GetSize(T x) => GetRoot(x)?.Size;

	public bool Unite(T x, T y)
	{
		var nx = GetRoot(x) ?? Add(x);
		var ny = GetRoot(y) ?? Add(y);
		if (nx == ny) return false;

		if (nx.Size < ny.Size) Merge(ny, nx);
		else Merge(nx, ny);
		return true;
	}

	void Merge(Node nx, Node ny)
	{
		ny.Parent = nx;
		nx.Size += ny.Size;
		--GroupsCount;
	}
}
