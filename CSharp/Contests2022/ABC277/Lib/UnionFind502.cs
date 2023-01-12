using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLab.DataTrees.UF502
{
	// typed vertexes, data augmentation
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

		public Node Find(TVertex x) => nodes.ContainsKey(x) ? Find(nodes[x]) : null;
		Node Find(Node n) => n.Parent == null ? n : n.Parent = Find(n.Parent);

		public bool AreSame(TVertex x, TVertex y)
		{
			if (nodes.Comparer.Equals(x, y)) return true;
			var nx = Find(x);
			var ny = Find(y);
			return nx != null && nx == ny;
		}
		public int? GetSize(TVertex x) => Find(x)?.Size;
		public TValue GetValue(TVertex x)
		{
			var n = Find(x);
			return n != null ? n.Value : iv;
		}

		public bool Union(TVertex x, TVertex y)
		{
			var nx = Find(x) ?? Add(x, iv);
			var ny = Find(y) ?? Add(y, iv);
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

		public ILookup<Node, Node> ToGroups() => nodes.Values.ToLookup(Find);
	}
}
