class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, a, b) = Read3();
		var d = ReadL();

		long ab = a + b;
		d = d.Select(x => x % ab).Distinct().ToArray();
		Array.Sort(d);
		n = d.Length;

		return Enumerable.Range(0, n).Any(i => (d[(i + n - 1) % n] - d[i] + ab) % ab <= a - 1);
	}
}
