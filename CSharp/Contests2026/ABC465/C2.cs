using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var g = new ListUnweightedGraph(n + 2);
		var sv = 1;

		for (int v = 1; v <= n; v++)
		{
			if (s[v - 1] == 'o')
			{
				g.AddEdge(sv, v + 1, true);
				sv = v;
			}
			else
			{
				g.AddEdge(v, v + 1, true);
			}
		}

		var tree = g.DFSTree(sv);
		var r = tree[n + 1].GetPathVertexes();
		return string.Join(" ", r[..n]);
	}
}
