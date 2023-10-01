using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var t = NewArray2(h, w, '.');

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (s[i][j] == '>')
				{
					t[i][j] = '#';

					for (int k = j + 1; k < w && s[i][k] == '.'; k++)
						t[i][k] = '#';
				}
				else if (s[i][j] == '<')
				{
					t[i][j] = '#';

					for (int k = j - 1; k >= 0 && s[i][k] == '.'; k--)
						t[i][k] = '#';
				}
				else if (s[i][j] == 'v')
				{
					t[i][j] = '#';

					for (int k = i + 1; k < h && s[k][j] == '.'; k++)
						t[k][j] = '#';
				}
				else if (s[i][j] == '^')
				{
					t[i][j] = '#';

					for (int k = i - 1; k >= 0 && s[k][j] == '.'; k--)
						t[k][j] = '#';
				}
				else if (s[i][j] == '#')
				{
					t[i][j] = '#';
				}
			}
		}

		var sg = new CharUnweightedGrid(s);
		var (si, sj) = sg.FindCell('S');
		var (gi, gj) = sg.FindCell('G');
		var sv = sg.ToVertexId(si, sj);
		var gv = sg.ToVertexId(gi, gj);

		var tg = new CharUnweightedGrid(t);
		var r = tg.ShortestByBFS(sv, gv);

		if (r[gv] == long.MaxValue) return -1;
		return r[gv];
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
