using CoderLib8.Collections;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var g = new LinkedListGraph(n + 1);
		foreach (var (c, p) in qs)
		{
			g.RemoveEdgeBefore(c);
			g.AddEdge(p, c);
		}

		var a = new int[n + 1];
		for (int v = 1; v <= n; v++)
		{
			if (g.Nodes[v].Left != null) continue;
			a[v] = g.GetPath(v).Count();
		}
		return string.Join(" ", a[1..]);
	}
}
