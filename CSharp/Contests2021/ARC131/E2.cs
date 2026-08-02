class E2
{
	static readonly Random random = new();
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		if (n < 6 || n % 3 == 2) return "No";

		var colors = "RBW";
		var ls = GetCounts(n);

		var s = Enumerable.Range(0, n - 1)
			.Select(i => new char[n])
			.ToArray();

		for (int k = 0; k < 3; k++)
		{
			foreach (var c in ls[k])
			{
				for (int i = 0; i < c; i++)
				{
					s[i][c] = colors[k];
				}
			}
		}

		return "Yes\n" + string.Join("\n", Enumerable.Range(0, n - 1).Select(i => new string(s[i][(i + 1)..])));

		static List<int>[] GetCounts(int n)
		{
			var sum = n * (n - 1) / 6;
			while (true)
			{
				var ls = NewCounts(n);
				if (ls.All(l => l.Sum() == sum)) return ls;
			}
		}

		static List<int>[] NewCounts(int n)
		{
			var ls = Array.ConvertAll(new bool[3], _ => new List<int>());
			for (int i = 1; i < n; i++)
				ls[random.Next(3)].Add(i);
			return ls;
		}
	}
}
