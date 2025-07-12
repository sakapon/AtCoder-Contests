class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = ReadL();

		Array.Sort(a);
		return Enumerable.Range(0, n - 1).Select(i => a[i + 1] - a[i]).OrderBy(x => x).Take(n - m).Sum();
	}
}
