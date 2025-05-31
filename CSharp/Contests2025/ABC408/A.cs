class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, s) = Read2();
		var t = Read().Prepend(0).ToArray();

		return Enumerable.Range(0, n).Max(i => t[i + 1] - t[i]) <= s;
	}
}
