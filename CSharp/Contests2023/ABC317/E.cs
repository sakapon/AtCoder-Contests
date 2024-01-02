using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var t = Array.ConvertAll(s, r => r.Select(c => c == '#' ? '#' : '.').ToArray());

		for (int i = 0; i < h; i++)
		{
			var f = false;
			for (int j = 0; j < w; j++)
			{
				if (s[i][j] == '>')
				{
					f = true;
					t[i][j] = '#';
				}
				else if (s[i][j] == '.')
				{
					if (f) t[i][j] = '#';
				}
				else
				{
					f = false;
				}
			}

			f = false;
			for (int j = w - 1; j >= 0; j--)
			{
				if (s[i][j] == '<')
				{
					f = true;
					t[i][j] = '#';
				}
				else if (s[i][j] == '.')
				{
					if (f) t[i][j] = '#';
				}
				else
				{
					f = false;
				}
			}
		}

		for (int j = 0; j < w; j++)
		{
			var f = false;
			for (int i = 0; i < h; i++)
			{
				if (s[i][j] == 'v')
				{
					f = true;
					t[i][j] = '#';
				}
				else if (s[i][j] == '.')
				{
					if (f) t[i][j] = '#';
				}
				else
				{
					f = false;
				}
			}

			f = false;
			for (int i = h - 1; i >= 0; i--)
			{
				if (s[i][j] == '^')
				{
					f = true;
					t[i][j] = '#';
				}
				else if (s[i][j] == '.')
				{
					if (f) t[i][j] = '#';
				}
				else
				{
					f = false;
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
}
