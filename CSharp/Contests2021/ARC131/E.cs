class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		if (n < 6 || n % 3 == 2) return "No";

		var colors = "RBW";
		var ls = Array.ConvertAll(new bool[3], _ => new List<int>());
		SetCounts(n);

		var s = Enumerable.Range(1, n - 1)
			.Select(i => new char[i])
			.Reverse()
			.ToArray();

		for (int k = 0; k < 3; k++)
		{
			foreach (var c in ls[k])
			{
				for (int i = 0; i < c; i++)
				{
					s[i][c - i - 1] = colors[k];
				}
			}
		}

		return "Yes\n" + string.Join("\n", s.Select(r => new string(r)));

		void SetCounts(int n)
		{
			if (n == 6)
			{
				ls[0].AddRange(new[] { 5 });
				ls[1].AddRange(new[] { 4, 1 });
				ls[2].AddRange(new[] { 3, 2 });
			}
			else if (n == 7)
			{
				ls[0].AddRange(new[] { 6, 1 });
				ls[1].AddRange(new[] { 5, 2 });
				ls[2].AddRange(new[] { 4, 3 });
			}
			else if (n == 9)
			{
				ls[0].AddRange(new[] { 8, 4 });
				ls[1].AddRange(new[] { 7, 5 });
				ls[2].AddRange(new[] { 6, 3, 2, 1 });
			}
			else if (n == 10)
			{
				ls[0].AddRange(new[] { 9, 6 });
				ls[1].AddRange(new[] { 8, 7 });
				ls[2].AddRange(new[] { 5, 4, 3, 2, 1 });
			}
			else
			{
				ls[0].AddRange(new[] { n - 1, n - 6 });
				ls[1].AddRange(new[] { n - 2, n - 5 });
				ls[2].AddRange(new[] { n - 3, n - 4 });
				SetCounts(n - 6);
			}
		}
	}
}
