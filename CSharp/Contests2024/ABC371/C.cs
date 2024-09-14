class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int u, int v) Read2() { var a = Read(); return (a[0] - 1, a[1] - 1); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var mg = int.Parse(Console.ReadLine());
		var esg = Array.ConvertAll(new bool[mg], _ => Read2());
		var mh = int.Parse(Console.ReadLine());
		var esh = Array.ConvertAll(new bool[mh], _ => Read2());
		var a = Enumerable.Range(0, n - 1).Select(i => new int[i + 1].Concat(Read()).ToArray()).ToArray();

		var r = 1 << 30;
		var p = new int[n];
		for (int i = 0; i < n; ++i) p[i] = i;

		do
		{
			var set = esg
				.Select(e => (u: p[e.u], v: p[e.v]))
				.Select(e => e.u < e.v ? e : (u: e.v, v: e.u))
				.ToHashSet();
			set.SymmetricExceptWith(esh);
			var s = set.Sum(e => a[e.u][e.v]);
			r = Math.Min(r, s);
		}
		while (NextPermutation(p));

		return r;
	}

	public static bool NextPermutation(int[] p)
	{
		var n = p.Length;

		// p[i] < p[i + 1] を満たす最大の i
		var i = n - 2;
		while (i >= 0 && p[i] >= p[i + 1]) --i;
		if (i < 0) return false;

		// p[i] < p[j] を満たす最大の j
		var j = i + 1;
		while (j + 1 < n && p[i] < p[j + 1]) ++j;

		(p[i], p[j]) = (p[j], p[i]);
		Array.Reverse(p, i + 1, n - i - 1);
		return true;
	}
}
