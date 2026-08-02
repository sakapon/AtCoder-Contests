class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var neighbors = Array.ConvertAll(new bool[n + 2], _ => new List<int>());
		var sv = 1;

		void AddEdge(int u, int v)
		{
			neighbors[u].Add(v);
			neighbors[v].Add(u);
		}

		void RemoveEdge(int u, int v)
		{
			neighbors[u].Remove(v);
			neighbors[v].Remove(u);
		}

		for (int v = 1; v <= n; v++)
		{
			AddEdge(v, v + 1);
		}

		for (int k = 1; k <= n; k++)
		{
			if (s[k - 1] == 'x') continue;

			RemoveEdge(k, k + 1);
			AddEdge(sv, k + 1);
			sv = k;
		}

		var r = new List<int>();
		for (int v = sv, pv = -1; v != -1;)
		{
			r.Add(v);
			var next = neighbors[v].FirstOrDefault(n => n != pv, -1);
			pv = v;
			v = next;
		}

		return string.Join(" ", r[..n]);
	}
}
