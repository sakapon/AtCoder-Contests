class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, q) = Read2();
		var x = Read();

		var r = new List<int>();
		var b = new int[n + 1];
		b[0] = 1 << 30;
		var rn = Enumerable.Range(1, n).ToArray();

		foreach (var v in x)
		{
			var u = v;
			if (v == 0)
			{
				var min = b.Min();
				u = rn.First(i => b[i] == min);
			}
			b[u]++;
			r.Add(u);
		}
		return string.Join(" ", r);
	}
}
