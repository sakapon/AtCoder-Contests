using System;
using System.Collections.Generic;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int i = 0; i < n; i++)
		{
			map[a[i]].Add(i);
		}

		var r = 0L;

		foreach (var l in map)
		{
			var m = l.Count;

			for (int j = 1; j < m; j++)
			{
				r += (long)(l[j] - l[j - 1] - 1) * j * (m - j);
			}
		}
		return r;
	}
}
