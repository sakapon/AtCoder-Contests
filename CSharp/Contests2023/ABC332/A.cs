class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int p, int q) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, s, k) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var r = ps.Sum(p => p.p * p.q);
		if (r < s) r += k;
		return r;
	}
}
