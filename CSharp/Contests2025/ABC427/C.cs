class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var map = new bool[n, n];
		foreach (var (u, v) in es)
		{
			map[u - 1, v - 1] = true;
			map[v - 1, u - 1] = true;
		}

		var r = 1000;
		for (uint x = 0; x < 1 << n; x++)
		{
			var g0 = new List<int>();
			var g1 = new List<int>();
			for (int i = 0; i < n; i++)
			{
				if ((x & (1 << i)) == 0) g0.Add(i);
				else g1.Add(i);
			}

			var c = 0;
			foreach (var i in g0)
				foreach (var j in g0)
					if (map[i, j]) c++;
			foreach (var i in g1)
				foreach (var j in g1)
					if (map[i, j]) c++;
			c /= 2;
			r = Math.Min(r, c);
		}
		return r;
	}
}
