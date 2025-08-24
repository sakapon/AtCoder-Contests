class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var map = new int[2 * n + 1];
		foreach (var (a, b) in ps)
		{
			map[a] = b;
			map[b] = a;
		}

		var q = new Stack<int>();

		for (int i = 1; i <= 2 * n; i++)
		{
			var j = map[i];
			if (q.TryPeek(out var q0) && q0 == j)
			{
				q.Pop();
			}
			else
			{
				q.Push(i);
			}
		}

		return q.Count > 0;
	}
}
