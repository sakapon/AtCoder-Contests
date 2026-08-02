class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());

		for (int i = 1; i <= n; i++)
		{
			var (a, b) = ps[i - 1];
			map[a].Add(i);
			if (b != 0) map[b].Add(i);
		}

		var u = new bool[n + 1];
		var q = new Queue<int>(map[0]);

		foreach (var v in map[0])
			u[v] = true;

		while (q.Count > 0)
		{
			var v = q.Dequeue();

			foreach (var nv in map[v])
			{
				if (u[nv]) continue;
				u[nv] = true;
				q.Enqueue(nv);
			}
		}

		return u.Count(b => b);
	}
}
