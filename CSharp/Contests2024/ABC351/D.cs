class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s0 = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var n = h * w;
		var s = s0.SelectMany(r => r).ToArray();
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());

		for (int i = 0; i < h; i++)
			for (int j = 1; j < w; j++)
			{
				var v = w * i + j;
				if (s[v] == '#' || s[v - 1] == '#')
				{
					if (s[v] == '.') s[v] = '?';
					if (s[v - 1] == '.') s[v - 1] = '?';
					continue;
				}
				map[v].Add(v - 1);
				map[v - 1].Add(v);
			}
		for (int j = 0; j < w; j++)
			for (int i = 1; i < h; i++)
			{
				var v = w * i + j;
				if (s[v] == '#' || s[v - w] == '#')
				{
					if (s[v] == '.') s[v] = '?';
					if (s[v - w] == '.') s[v - w] = '?';
					continue;
				}
				map[v].Add(v - w);
				map[v - w].Add(v);
			}

		var r = 1;
		var u = new int[n];
		var q = new Queue<int>();
		Array.Fill(u, -1);

		for (int v = 0; v < n; v++)
		{
			if (s[v] != '.') continue;
			if (u[v] != -1) continue;
			var c = Count(v);
			if (r < c) r = c;
		}
		return r;

		int Count(int sv)
		{
			var c = 1;
			u[sv] = sv;
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();

				foreach (var nv in map[v])
				{
					if (u[nv] == sv) continue;
					c++;
					u[nv] = sv;
					if (s[nv] == '?') continue;
					q.Enqueue(nv);
				}
			}
			return c;
		}
	}
}
