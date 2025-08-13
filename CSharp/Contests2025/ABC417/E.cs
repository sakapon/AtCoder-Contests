using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var (n, m, x, y) = Read4();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var g = new ListUnweightedGraph(n + 1, es, true);
		foreach (var l in g.AdjacencyList)
			l.Sort();

		var r = g.DFSTree(x, y);
		var p = r[y].GetPathVertexes();
		return string.Join(" ", p);
	}
}
