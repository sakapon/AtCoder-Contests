class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var dp = new bool[1 << n];

		for (uint x = 0; x < 1 << n; x++)
		{
			if (dp[x]) continue;

			for (int i = 0; i < n; i++)
			{
				if ((x & (1U << i)) != 0) continue;

				for (int j = i + 1; j < n; j++)
				{
					if ((x & (1U << j)) != 0) continue;

					if (ps[i].a == ps[j].a || ps[i].b == ps[j].b)
					{
						dp[x | (1U << i) | (1U << j)] = true;
					}
				}
			}
		}
		return dp[^1] ? "Takahashi" : "Aoki";
	}
}
