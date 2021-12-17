using System;
using System.Collections.Generic;
using System.Numerics;

class EB
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var k = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var r = 0L;
		var u = new HashSet<ulong>();
		var q = new Queue<ulong>();
		q.Enqueue(0);

		while (q.Count > 0)
		{
			var x = q.Dequeue();
			if (u.Contains(x)) continue;
			u.Add(x);

			if (BitOperations.PopCount(x) == k)
			{
				r++;
				continue;
			}

			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					if (s[i][j] == '#') continue;

					var id = i * n + j;
					var f = 1UL << id;
					if ((x & f) != 0) continue;

					if (i != 0 && (x & (f >> n)) != 0 ||
						i != n - 1 && (x & (f << n)) != 0 ||
						j != 0 && (x & (f >> 1)) != 0 ||
						j != n - 1 && (x & (f << 1)) != 0 ||
						x == 0)
						q.Enqueue(x | f);
				}
			}
		}

		return r;
	}
}
