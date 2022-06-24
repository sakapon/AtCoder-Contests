using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.DataTrees.Typed
{
	// Test: https://atcoder.jp/contests/past202010-open/tasks/past202010_m
	// Test: https://atcoder.jp/contests/abc183/tasks/abc183_f
	// typed vertexes
	// 頂点を動的に追加できる方式
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}, GroupsCount = {GroupsCount}")]
	public class UnionFind<TVertex, TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
		public class Node
		{
			public TVertex Item;
			public TValue Value;
			public Node Parent;
			public int Size = 1;
		}

		Dictionary<TVertex, Node> nodes = new Dictionary<TVertex, Node>();
		public int Count => nodes.Count;
		public int GroupsCount { get; private set; }
		TValue iv;
		Func<TValue, TValue, TValue> mergeValues;

		public UnionFind(TValue iv, Func<TValue, TValue, TValue> mergeValues, IEnumerable<KeyValuePair<TVertex, TValue>> collection = null)
		{
			if (collection != null)
				foreach (var p in collection) nodes[p.Key] = new Node { Item = p.Key, Value = p.Value };
			GroupsCount = nodes.Count;
			this.iv = iv;
			this.mergeValues = mergeValues;
		}

		public Node Add(TVertex x, TValue value)
		{
			if (nodes.ContainsKey(x)) return null;
			++GroupsCount;
			return nodes[x] = new Node { Item = x, Value = value };
		}

		public Node GetRoot(TVertex x) => nodes.ContainsKey(x) ? GetRoot(nodes[x]) : null;
		Node GetRoot(Node n) => n.Parent == null ? n : n.Parent = GetRoot(n.Parent);

		public bool AreUnited(TVertex x, TVertex y)
		{
			if (nodes.Comparer.Equals(x, y)) return true;
			var nx = GetRoot(x);
			var ny = GetRoot(y);
			return nx != null && nx == ny;
		}
		public int? GetSize(TVertex x) => GetRoot(x)?.Size;
		public TValue GetValue(TVertex x)
		{
			var n = GetRoot(x);
			return n != null ? n.Value : iv;
		}

		public bool Unite(TVertex x, TVertex y)
		{
			var nx = GetRoot(x) ?? Add(x, iv);
			var ny = GetRoot(y) ?? Add(y, iv);
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
			nx.Value = mergeValues(nx.Value, ny.Value);
		}

		public ILookup<Node, Node> ToGroups() => nodes.Values.ToLookup(GetRoot);
	}
}
