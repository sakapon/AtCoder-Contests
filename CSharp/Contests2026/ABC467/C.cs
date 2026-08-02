class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var b = Read();

		var a2 = new int[n];
		for (int i = 1; i < n; i++)
			a2[i] = a2[i - 1] == b[i - 1] ? 0 : 1;

		var c = a.Zip(a2).Count(p => p.First == p.Second);
		return Math.Min(c, n - c);
	}
}
