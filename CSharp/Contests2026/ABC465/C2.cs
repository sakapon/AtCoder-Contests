class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var nodes = Enumerable.Range(0, n + 2).Select(v => new Node(v)).ToArray();
		var sv = 1;

		void AddEdge(int u, int v)
		{
			nodes[u].Neighbors.Add(nodes[v]);
			nodes[v].Neighbors.Add(nodes[u]);
		}

		void RemoveEdge(int u, int v)
		{
			nodes[u].Neighbors.Remove(nodes[v]);
			nodes[v].Neighbors.Remove(nodes[u]);
		}

		for (int v = 1; v <= n; v++)
		{
			AddEdge(v, v + 1);
		}

		for (int k = 1; k <= n; k++)
		{
			if (s[k - 1] == 'x') continue;

			RemoveEdge(k, k + 1);
			AddEdge(sv, k + 1);
			sv = k;
		}

		var r = new List<int>();
		for (Node node = nodes[sv], pNode = null; node != null;)
		{
			r.Add(node.Id);
			var next = node.Neighbors.FirstOrDefault(n => n != pNode);
			pNode = node;
			node = next;
		}

		return string.Join(" ", r[..n]);
	}
}

class Node(int id)
{
	public int Id = id;
	public List<Node> Neighbors = [];
}
