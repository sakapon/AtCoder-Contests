using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int r, int c, int e) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read3());

		var rn = Enumerable.Range(0, n).ToArray();

		var grid = new CharUnweightedGrid(s);
		var sv = grid.FindVertexId('S');
		var ev = grid.FindVertexId('T');

		var ps2 = ps.Select(p => (v: grid.ToVertexId(p.r - 1, p.c - 1), p.e)).ToArray();
		var medIndexes = rn.ToDictionary(i => ps2[i].v);

		if (!medIndexes.ContainsKey(sv)) return false;
		var si = medIndexes[sv];
		var ei = n;

		var map = rn.Select(GetMedDest).Append(new List<int>()).ToArray();
		var graph = new ListUnweightedGraph(map);
		return graph.ConnectivityByDFS(si, ei)[ei];

		List<int> GetMedDest(int mi)
		{
			var d = grid.ShortestByBFS(ps2[mi].v);
			var e = ps[mi].e;

			var l = rn.Where(mj => d[ps2[mj].v] <= e).ToList();
			if (d[ev] <= e) l.Add(ei);
			return l;
		}
	}
}
