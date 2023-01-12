using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		const int nMax = 1 << 12;
		var d = new Dictionary<(int, int), int>();
		var lrs = new List<(int, int)>();

		for (int len = 2; len < nMax; len <<= 1)
		{
			for (int c = len; c < nMax; c += len)
			{
				for (int i = len >> 1; i < len; i++)
				{
					if (c <= n)
					{
						d[(c - i, c)] = lrs.Count;
						lrs.Add((c - i, c));
					}
					if (c + i <= n)
					{
						d[(c, c + i)] = lrs.Count;
						lrs.Add((c, c + i));
					}
				}
			}
		}
		for (int i = 1; i < nMax; i++)
		{
			//d[(nMax - i, nMax)] = lrs.Count;
			//lrs.Add((nMax - i, nMax));
			if (i <= n)
			{
				d[(0, i)] = lrs.Count;
				lrs.Add((0, i));
			}
		}
		//d[(0, nMax)] = lrs.Count;
		//lrs.Add((0, nMax));

		Console.WriteLine(lrs.Count);
		Console.WriteLine(string.Join("\n", lrs.Select(lr => $"{lr.Item1 + 1} {lr.Item2}")));

		var qc = int.Parse(Console.ReadLine());

		while (qc-- > 0)
		{
			var (L, R) = Read2();
			L--;

			var (a, b) = Get2Sets(L, R);
			Console.WriteLine($"{a + 1} {b + 1}");

			(int, int) Get2Sets(int l, int r)
			{
				if ((l, r) == (0, nMax)) return (d[(L, R)], d[(L, R)]);

				for (int f = 1; l < r; f <<= 1)
				{
					if ((l & f) != 0) l += f;
					if ((r & f) != 0) r -= f;
				}
				if (l == L || r == R) return (d[(L, R)], d[(L, R)]);
				return (d[(L, r)], d[(l, R)]);
			}
		}
	}
}
