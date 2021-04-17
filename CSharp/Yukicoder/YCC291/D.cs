using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());

		var d = new Dictionary<int, List<(int, int)>>();

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				var k = a[i][j];
				if (k == 0) continue;

				if (!d.ContainsKey(k)) d[k] = new List<(int, int)>();
				d[k].Add((i, j));
			}
		}
		Console.WriteLine(d.Values.Sum(ForGroup));
	}

	static int ForGroup(List<(int, int)> ps)
	{
		var r = 0;
		var d = new Dictionary<int, HashSet<int>>();

		foreach (var (i, j) in ps)
		{
			var j2 = 500 + j;

			if (!d.ContainsKey(i)) d[i] = new HashSet<int>();
			d[i].Add(j2);

			if (!d.ContainsKey(j2)) d[j2] = new HashSet<int>();
			d[j2].Add(i);
		}

		while (d.Count > 0)
		{
			var (i, l) = d.OrderBy(p => -p.Value.Count).First();
			d.Remove(i);
			foreach (var j in l)
			{
				d[j].Remove(i);
				if (d[j].Count == 0) d.Remove(j);
			}
			r++;
		}
		return r;
	}
}
