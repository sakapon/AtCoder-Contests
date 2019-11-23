using System;
using System.Linq;

class C
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int h = a[0], w = a[1], k = a[2];
		var s = new int[h].Select(_ => Console.ReadLine()).ToArray();
		var none = new string('.', w);

		var m = new int[h, w];
		for (int t = 0, i = 0; i < h; i++)
		{
			if (s[i] == none) continue;

			var cw = s[i].IndexOf('#');
			for (int j = 0; j < cw; j++)
				m[i, j] = t + 1;
			for (int j = cw; j < w; j++)
				if (s[i][j] == '#') m[i, j] = ++t;
				else m[i, j] = t;
		}

		var ch = s.TakeWhile(x => x == none).Count();
		for (int i = 0; i < ch; i++)
			for (int j = 0; j < w; j++)
				m[i, j] = m[ch, j];
		for (int i = ch; i < h; i++)
		{
			if (s[i] != none) continue;

			for (int j = 0; j < w; j++)
				m[i, j] = m[i - 1, j];
		}

		for (int i = 0; i < h; i++)
			Console.WriteLine(string.Join(" ", Enumerable.Range(0, w).Select(j => m[i, j])));
	}
}
