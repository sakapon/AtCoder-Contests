class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read3());

		if (n % 2 == 1) return -1;

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<(int to, int x)>());
		foreach (var (u, v, x) in es)
		{
			map[u].Add((v, x));
			map[v].Add((u, x));
		}

		var used = new bool[n + 1];
		var values = new int[n + 1];

		void DFS(int v, int value)
		{
			if (used[v]) return;
			used[v] = true;
			values[v] = value;

			foreach (var (to, x) in map[v])
			{
				DFS(to, value ^ x);
			}
		}
		DFS(1, 0);

		return Enumerable.Range(1, n).Select(v => v ^ values[v]).Aggregate((x, y) => x ^ y);
	}
}
