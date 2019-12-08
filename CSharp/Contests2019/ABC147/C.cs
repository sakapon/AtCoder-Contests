using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => int.Parse(Console.ReadLine()))
			.Select(a => new int[a].Select(i => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray())
			.ToArray();

		var m = new int[n, n];
		for (int i = 0; i < n; i++)
			foreach (var xy in ps[i])
				m[i, xy[0] - 1] = xy[1] == 0 ? -1 : 1;

		var p2 = new int[n + 1];
		p2[0] = 1;
		for (int i = 1; i <= n; i++) p2[i] = 2 * p2[i - 1];

		var M = 0;
		for (int x = 0; x < p2[n]; x++)
		{
			var ok = true;
			var c = 0;
			for (int i = 0; i < n; i++)
			{
				if ((x & p2[i]) == 0) continue;
				c++;
				for (int j = 0; j < n; j++)
				{
					if (m[i, j] == 1 && (x & p2[j]) == 0) { ok = false; break; }
					if (m[i, j] == -1 && (x & p2[j]) > 0) { ok = false; break; }
				}
				if (!ok) break;
			}
			if (ok) M = Math.Max(M, c);
		}
		Console.WriteLine(M);
	}
}
