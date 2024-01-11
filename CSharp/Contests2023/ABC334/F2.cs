using CoderLib8.Values;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var s = (IntV)Read2L();
		var ps = Array.ConvertAll(new bool[n], _ => (IntV)Read2L());

		var rn = Enumerable.Range(0, n - 1).ToArray();
		var ds = rn.Select(i => (ps[i + 1] - ps[i]).Norm).ToArray();
		var es = rn.Select(i => (ps[i + 1] - s).Norm + (ps[i] - s).Norm - ds[i]).ToArray();

		// sliding min
		var min = 0.0;
		var a = new double[n];
		int l = 0, r = -1;
		var b = new int[n];
		for (int i = 0; i < n; ++i)
		{
			while (l <= r && a[b[r]] > a[i]) --r;
			b[++r] = i;
			if (b[l] == i - k) ++l;
			if (i < n - 1) a[i + 1] = a[b[l]] + es[i];
			else min = a[b[l]];
		}

		return ds.Sum() + (ps[0] - s).Norm + (ps[^1] - s).Norm + min;
	}
}
