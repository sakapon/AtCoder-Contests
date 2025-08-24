class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, l) = Read2();
		var d = Read();

		if (l % 3 != 0) return 0;

		var x = 0;
		var cs = new long[l];
		cs[x]++;

		foreach (var y in d)
		{
			x += y;
			x %= l;
			cs[x]++;
		}

		l /= 3;
		var r = 0L;
		for (int i = 0; i < l; i++)
			r += cs[i] * cs[i + l] * cs[i + 2 * l];
		return r;
	}
}
