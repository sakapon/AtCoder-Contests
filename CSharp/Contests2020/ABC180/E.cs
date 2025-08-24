using CoderLib6.Graphs;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y, int z) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read3());

		var d = TSP.NewArray2<long>(n, n);

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				d[i][j] = Math.Abs(ps[j].x - ps[i].x) + Math.Abs(ps[j].y - ps[i].y) + Math.Max(0, ps[j].z - ps[i].z);
			}
		}

		var sv = 0;
		var dp = TSP.Execute(n, sv, d);
		return dp[^1][sv];
	}
}
