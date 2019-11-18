using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = new int[n].Select(_ => Console.ReadLine()).ToArray();

		var map = s.Select(_ => new List<int>()).ToArray();
		for (int i = 0; i < n; i++)
			for (int j = 0; j < n; j++)
				if (s[i][j] == '1') map[i].Add(j);

		var M = 0;
		var q = new List<int>();
		var u = new int[n];
		for (int i = 0; i < n; i++)
		{
			q.Add(i);
			Array.Clear(u, 0, n);
			u[i] = 1;

			for (int k = 2; q.Any(); k++)
			{
				var ps = q.ToArray();
				q.Clear();

				foreach (var p in ps)
					foreach (var np in map[p])
					{
						if (u[np] == 0)
						{
							u[np] = k;
							q.Add(np);
						}
						else if (u[np] != k && u[np] + 1 != u[p]) { Console.WriteLine(-1); return; }
					}
			}
			M = Math.Max(M, u.Max());
		}
		Console.WriteLine(M);
	}
}
