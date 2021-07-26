using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class ED2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var k = int.Parse(Console.ReadLine());
		var s = new bool[n].SelectMany(_ => Console.ReadLine()).ToArray();

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

			if (x == 0)
			{
				for (int id = 0; id < n * n; id++)
				{
					if (s[id] == '#') continue;
					var f = 1UL << id;
					Dfs(f);
				}
				return;
			}

			for (int id = 0; id < n * n; id++)
			{
				var f = 1UL << id;
				if ((x & f) == 0) continue;

				var (i, j) = (id / n, id % n);
				if (i != 0 && s[id - n] != '#') Dfs(x | (f >> n));
				if (i != n - 1 && s[id + n] != '#') Dfs(x | (f << n));
				if (j != 0 && s[id - 1] != '#') Dfs(x | (f >> 1));
				if (j != n - 1 && s[id + 1] != '#') Dfs(x | (f << 1));
			}
		}
	}
}
