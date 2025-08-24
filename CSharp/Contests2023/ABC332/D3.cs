using CoderLib8.Graphs.SPPs.SPPs101;
using CoderLib8.Values;

class D3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());
		var b = Array.ConvertAll(new bool[h], _ => Read());

		var sv = new EquatableArray<int>(a.SelectMany(x => x).ToArray());
		var ev = new EquatableArray<int>(b.SelectMany(x => x).ToArray());

		var r = UnweightedPathCoreTyped.ShortestByBFS(v =>
		{
			var nexts = new List<EquatableArray<int>>();
			for (int i = 1; i < h; i++)
			{
				nexts.Add(SwapRows(v, i - 1, i));
			}
			for (int j = 1; j < w; j++)
			{
				nexts.Add(SwapColumns(v, j - 1, j));
			}
			return nexts.ToArray();
		}, sv, ev);

		if (!r.ContainsKey(ev)) return -1;
		return r[ev];

		EquatableArray<T> SwapRows<T>(EquatableArray<T> a, int i1, int i2)
		{
			var r = a.Clone();
			for (int j = 0; j < w; ++j)
			{
				r[i1 * w + j] = a[i2 * w + j];
				r[i2 * w + j] = a[i1 * w + j];
			}
			return r;
		}

		EquatableArray<T> SwapColumns<T>(EquatableArray<T> a, int j1, int j2)
		{
			var r = a.Clone();
			for (int i = 0; i < h; ++i)
			{
				r[i * w + j1] = a[i * w + j2];
				r[i * w + j2] = a[i * w + j1];
			}
			return r;
		}
	}
}
