using System;
using System.Collections.Generic;
using Bang.Graphs.Int.Spp;

class Q043
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var sp = GraphConsole.ReadPoint();
		var ep = GraphConsole.ReadPoint();
		var s = GraphConsole.ReadEnclosedGrid(ref h, ref w);

		int ToId(Point p) => p.i * w + p.j;
		int ToId2(int i, int j) => i * w + j;
		Point FromId(int id) => new Point(id / w, id % w);

		var u_tate = new bool[h, w];
		var u_yoko = new bool[h, w];

		var r = ShortestPathCore.Bfs(h * w, pid =>
		{
			var (pi, pj) = FromId(pid);

			var nexts = new List<int>();

			if (!u_tate[pi, pj])
			{
				u_tate[pi, pj] = true;
				for (int i = pi - 1; i >= 0 && s[i][pj] == '.'; i--)
				{
					u_tate[i, pj] = true;
					nexts.Add(ToId2(i, pj));
				}
				for (int i = pi + 1; i < h && s[i][pj] == '.'; i++)
				{
					u_tate[i, pj] = true;
					nexts.Add(ToId2(i, pj));
				}
			}
			if (!u_yoko[pi, pj])
			{
				u_yoko[pi, pj] = true;
				for (int j = pj - 1; j >= 0 && s[pi][j] == '.'; j--)
				{
					u_yoko[pi, j] = true;
					nexts.Add(ToId2(pi, j));
				}
				for (int j = pj + 1; j < w && s[pi][j] == '.'; j++)
				{
					u_yoko[pi, j] = true;
					nexts.Add(ToId2(pi, j));
				}
			}

			return nexts.ToArray();
		},
		ToId(sp), ToId(ep));

		return r[ToId(ep)] - 1;
	}
}
