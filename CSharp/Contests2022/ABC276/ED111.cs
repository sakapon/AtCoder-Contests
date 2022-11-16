using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Arrays.Grid111;
using CoderLib8.Graphs.SPPs.Arrays.PathCore111;

class ED111
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var c = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var grid = new CharGrid(c);
		var (si, sj) = grid.FindCell('S');
		var sv = grid.ToVertexId(si, sj);
		grid[si][sj] = '#';

		var map = grid.GetUnweightedAdjacencyList().ToArrays();
		var svs = grid.GetUnweightedNexts(sv);

		for (int i = 0; i < svs.Length - 1; i++)
		{
			var u = map.ConnectivityByDFS(svs[i]);

			for (int j = i + 1; j < svs.Length; j++)
			{
				if (u[svs[j]]) return true;
			}
		}
		return false;
	}
}
