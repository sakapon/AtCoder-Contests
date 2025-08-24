class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[m], _ => Read2());

		var a = new int[n + 2];
		foreach (var (l, r) in ps)
		{
			a[l]++;
			a[r + 1]--;
		}
		for (int i = 0; i < n; i++)
		{
			a[i + 1] += a[i];
		}
		return a[1..(n + 1)].Min();
	}
}
