using System;
using System.Collections.Generic;

public class HLD
{
	public static List<int>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}

	[System.Diagnostics.DebuggerDisplay(@"\{{Id}\}")]
	public class Node
	{
		public int Id;
		public int Size = 1, Depth, Order;
		public Node Parent, HeavyChild;
		public Group Group;
	}

	[System.Diagnostics.DebuggerDisplay(@"\{{Root.Id}, Count = {Count}\}")]
	public class Group
	{
		public List<Group> Children = new List<Group>();
		public Group Parent;
		public int Depth;

		public List<Node> Nodes = new List<Node>();
		public int Count => Nodes.Count;
		public Node Root => Nodes[0];
	}

	public int[][] Map { get; }
	public Node[] Nodes { get; }
	public Node[] NodesByOrder { get; }
	public List<Group> Groups { get; } = new List<Group>();

	public HLD(int n, int[][] map, int root)
	{
		Map = map;
		Nodes = new Node[n];
		for (int i = 0; i < n; ++i) Nodes[i] = new Node { Id = i };

		Dfs(Nodes[root], new Node { Id = -1 });

		NodesByOrder = new Node[n];
		var order = -1;
		foreach (var g in Groups)
		{
			g.Nodes.Reverse();
			foreach (var cn in g.Nodes)
			{
				cn.Order = ++order;
				NodesByOrder[order] = cn;
			}
			if (g.Root.Parent != null)
			{
				g.Parent = g.Root.Parent.Group;
				g.Parent.Children.Add(g);
			}
		}
		DfsForGroup(Nodes[root].Group);
	}

	void Dfs(Node cn, Node pn)
	{
		Node hc = null;

		foreach (var nv in Map[cn.Id])
		{
			if (nv == pn.Id) continue;
			var nn = Nodes[nv];
			nn.Depth = cn.Depth + 1;
			nn.Parent = cn;
			Dfs(nn, cn);
			cn.Size += nn.Size;
			if ((hc?.Size ?? 0) < nn.Size) hc = nn;
		}

		cn.HeavyChild = hc;
		if (hc == null) Groups.Add(cn.Group = new Group());
		else cn.Group = hc.Group;
		cn.Group.Nodes.Add(cn);
	}

	void DfsForGroup(Group g)
	{
		foreach (var ng in g.Children)
		{
			ng.Depth = g.Depth + 1;
			DfsForGroup(ng);
		}
	}
}
