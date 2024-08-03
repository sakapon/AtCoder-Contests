class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		// RPS : 012
		const string RPS = "RPS";

		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Select(c => RPS.IndexOf(c)).ToArray();

		var dp = new int[3];
		var dt = new int[3];

		for (int i = 0; i < n; i++)
		{
			Array.Fill(dt, -1);

			for (int j = 0; j < 2; j++)
			{
				var te = (s[i] + j) % 3;

				for (int te0 = 0; te0 < 3; te0++)
				{
					if (te0 == te) continue;
					if (dp[te0] == -1) continue;
					Chmax(ref dt[te], dp[te0] + j);
				}
			}

			(dp, dt) = (dt, dp);
		}
		return dp.Max();
	}

	public static int Chmax(ref int x, int v) => x < v ? x = v : x;
}
