using CoderLib8.Collections.Statics.Typed;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		Array.Sort(a);
		var set = new ArrayItemSet<int>(a);
		return string.Join("\n", qs.Select(q => Query(q.Item1, q.Item2)));

		int Query(int b, int k)
		{
			return First(0, 1 << 29, d => set.GetCount(b - d, b + d + 1) >= k);
		}
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
