using CoderLib8.Graphs.Arrays;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var d = WeightedDiameter(n + 1, 1, es);
		return es.Sum(e => (long)e[2]) * 2 - d;
	}

	public static long WeightedDiameter(int n, int root, int[][] ues)
	{
		var tree = new WeightedTree(n, root, ues);

		var (mv, md) = (-1, -1L);
		for (int v = 0; v < n; v++)
		{
			var d = tree.Costs[v];
			if (md < d) (mv, md) = (v, d);
		}
		return new WeightedTree(n, mv, tree.Map).Costs.Max();
	}
}
