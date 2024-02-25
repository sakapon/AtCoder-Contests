class B2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var inv = new int[n + 1];
		for (int i = 0; i < n; i++)
			inv[p[i]] = i;

		var r = new List<int>();
		foreach (var (a, b) in qs)
			r.Add(inv[a] < inv[b] ? a : b);
		return string.Join("\n", r);
	}
}
