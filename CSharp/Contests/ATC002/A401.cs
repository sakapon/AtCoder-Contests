﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Int.UnweightedGraph401;

class A401
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var (si, sj) = Read2();
		var (ei, ej) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var grid = new CharUnweightedGrid(s);

		si--; sj--;
		ei--; ej--;
		var sv = grid.ToVertexId(si, sj);
		var ev = grid.ToVertexId(ei, ej);

		var r = grid.ShortestByBFS(sv, ev);
		return r[ev].Cost;
	}
}
