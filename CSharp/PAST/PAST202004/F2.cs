using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		foreach (var (a, b) in ps)
		{
			map[a].Add(b);
		}

		var r = new int[n + 1];
		var counts = new int[101];

		for (int i = 1; i <= n; i++)
		{
			foreach (var b in map[i])
			{
				counts[b]++;
			}

			for (int j = 100; j > 0; j--)
			{
				if (counts[j] > 0)
				{
					r[i] = r[i - 1] + j;
					counts[j]--;
					break;
				}
			}
		}

		Console.WriteLine(string.Join("\n", r.Skip(1)));
	}
}
