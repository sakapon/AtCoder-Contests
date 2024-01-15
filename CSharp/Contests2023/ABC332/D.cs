using CoderLib8.Graphs.SPPs.SPPs101;
using CoderLib8.Values;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());
		var b = Array.ConvertAll(new bool[h], _ => Read());

		var sv = new Grid2<int>(h, w, a.SelectMany(x => x).ToArray());
		var ev = new Grid2<int>(h, w, b.SelectMany(x => x).ToArray());

		var r = UnweightedPathCoreTyped.ShortestByBFS(v =>
		{
			var nexts = new List<Grid2<int>>();
			for (int i = 1; i < h; i++)
			{
				nexts.Add(v.SwapRows(i - 1, i));
			}
			for (int j = 1; j < w; j++)
			{
				nexts.Add(v.SwapColumns(j - 1, j));
			}
			return nexts.ToArray();
		}, sv, ev);

		if (!r.ContainsKey(ev)) return -1;
		return r[ev];
	}
}
