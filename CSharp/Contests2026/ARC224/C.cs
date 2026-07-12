using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var g = new ListUnweightedGraph(n + 1, es, true);
		var r = g.DFSTree(1);

		return string.Join(" ", r.Vertexes[1..].Select(v => v.Cost));
	}
}
