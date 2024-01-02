using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var yoko = NewArray2<int>(h, 26);
		var tate = NewArray2<int>(w, 26);

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				yoko[i][s[i][j] - 'a']++;
				tate[j][s[i][j] - 'a']++;
			}
		}

		var rh = h;
		var rw = w;
		var end_yoko = new int[26];
		var end_tate = new int[26];

		while (true)
		{
			var all_yoko = Find_yoko().ToArray();
			var all_tate = Find_tate().ToArray();
			if (all_yoko.Length == 0 && all_tate.Length == 0) break;

			foreach (var (i, c) in all_yoko)
			{
				yoko[i][c] -= rw;
				end_tate[c]++;
			}
			foreach (var (j, d) in all_tate)
			{
				tate[j][d] -= rh;
				end_yoko[d]++;
			}

			rh -= all_yoko.Length;
			rw -= all_tate.Length;
		}
		return rh * rw;

		IEnumerable<(int, int)> Find_yoko()
		{
			if (rw <= 1) yield break;

			for (int i = 0; i < h; i++)
			{
				for (int c = 0; c < 26; c++)
				{
					if (yoko[i][c] - end_yoko[c] == rw)
					{
						yield return (i, c);
						break;
					}
				}
			}
		}

		IEnumerable<(int, int)> Find_tate()
		{
			if (rh <= 1) yield break;

			for (int j = 0; j < w; j++)
			{
				for (int c = 0; c < 26; c++)
				{
					if (tate[j][c] - end_tate[c] == rh)
					{
						yield return (j, c);
						break;
					}
				}
			}
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
