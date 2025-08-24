class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var a = Read();
		var g = Read();

		return a[0] < g[3] && g[0] < a[3]
			&& a[1] < g[4] && g[1] < a[4]
			&& a[2] < g[5] && g[2] < a[5];
	}
}
