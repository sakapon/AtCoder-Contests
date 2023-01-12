using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Read());
		var n = int.Parse(Console.ReadLine());

		var rh = Enumerable.Range(0, h).Reverse().ToArray();
		var rw = Enumerable.Range(0, w).ToArray();

		var ls = Array.ConvertAll(rw, j => rh.Select(i => s[i][j]).TakeWhile(v => v > 0).ToList());

		while (n-- > 0)
		{
			var (r, c) = Read2();
			c--;
			r = h - r;
			if (r < ls[c].Count) ls[c].RemoveAt(r);
		}

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				s[i][j] = 0;
			}
		}

		for (int j = 0; j < w; j++)
		{
			var l = ls[j];
			for (int i = 0; i < l.Count; i++)
			{
				s[h - 1 - i][j] = l[i];
			}
		}

		foreach (var r in s)
		{
			Console.WriteLine(string.Join(" ", r));
		}
	}
}
