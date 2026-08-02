class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int a, int d, int b) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read3());

		var counts = new int[n + 1];
		foreach (var (a, d, b) in ps)
			counts[a]++;
		var colors = counts.Count(x => x > 0);

		var r = new List<int>();
		var q = new Queue<(int a, int d, int b)>(ps.OrderBy(p => p.d));

		for (int j = 1; j <= m; j++)
		{
			while (q.Count > 0 && q.Peek().d == j)
			{
				var (a, d, b) = q.Dequeue();
				if (--counts[a] == 0) colors--;
				if (counts[b]++ == 0) colors++;
			}
			r.Add(colors);
		}
		return string.Join("\n", r);
	}
}
