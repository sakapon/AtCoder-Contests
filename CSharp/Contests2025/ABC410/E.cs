class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, H, M) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var dp = new[] { (H, M) };
		var dt = new List<(int h, int m)>();
		var q = new Stack<(int h, int m)>();

		for (int i = 0; i < n; i++)
		{
			var (a, b) = ps[i];

			foreach (var (h, m) in dp)
			{
				if (h >= a) dt.Add((h - a, m));
				if (m >= b) dt.Add((h, m - b));
			}
			dt.Sort();

			foreach (var (h, m) in dt)
			{
				while (q.Count > 0)
				{
					var (h0, m0) = q.Peek();
					if (m0 <= m) q.Pop();
					else break;
				}
				q.Push((h, m));
			}

			dp = q.ToArray();
			dt.Clear();
			q.Clear();

			if (dp.Length == 0) return i;
		}
		return n;
	}
}
