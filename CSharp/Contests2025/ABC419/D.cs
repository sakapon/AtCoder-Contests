class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var s = Console.ReadLine();
		var t = Console.ReadLine();
		var ps = Array.ConvertAll(new bool[m], _ => Read2());

		var b = new bool[n + 1];
		foreach (var (l, r) in ps)
		{
			b[l - 1] ^= true;
			b[r] ^= true;
		}

		var cs = new char[n];
		var useT = false;

		for (int i = 0; i < n; i++)
		{
			useT ^= b[i];
			cs[i] = useT ? t[i] : s[i];
		}
		return new string(cs);
	}
}
