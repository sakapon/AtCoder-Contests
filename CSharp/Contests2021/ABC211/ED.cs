using System;
using System.Collections.Generic;
using System.Numerics;

class ED
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var k = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var r = 0L;
		var u = new HashSet<ulong>();

		Dfs(0);
		return r;

		void Dfs(ulong x)
		{
			if (u.Contains(x)) return;
			u.Add(x);

			if (BitOperations.PopCount(x) == k)
			{
				r++;
				return;
			}

			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					if (s[i][j] == '#') continue;

					var id = i * n + j;
					var f = 1UL << id;
					if ((x & f) != 0) continue;

					if (i - 1 >= 0 && (x & (f >> n)) != 0 ||
						i + 1 < n && (x & (f << n)) != 0 ||
						j - 1 >= 0 && (x & (f >> 1)) != 0 ||
						j + 1 < n && (x & (f << 1)) != 0 ||
						x == 0)
						Dfs(x | f);
				}
			}
		}
	}
}
