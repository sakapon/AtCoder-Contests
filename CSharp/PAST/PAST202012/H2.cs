﻿using System;
using System.Collections.Generic;

class H2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var (si, sj) = Read2();
		var s = ReadEnclosedGrid(ref h, ref w);

		Func<int, int, int> toHash = (i, j) => i * w + j;
		Func<int, (int, int)> fromHash = hash => (hash / w, hash % w);

		var r = Bfs(h * w, v =>
		{
			var (i, j) = fromHash(v);
			var nvs = new List<int>();
			char c;

			if ((c = s[i - 1][j]) == '.' || c == 'v') nvs.Add(toHash(i - 1, j));
			if ((c = s[i + 1][j]) == '.' || c == '^') nvs.Add(toHash(i + 1, j));
			if ((c = s[i][j - 1]) == '.' || c == '>') nvs.Add(toHash(i, j - 1));
			if ((c = s[i][j + 1]) == '.' || c == '<') nvs.Add(toHash(i, j + 1));

			return nvs.ToArray();
		}, toHash(si, sj));

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int i = 1; i < h - 1; i++)
		{
			for (int j = 1; j < w - 1; j++)
				Console.Write(s[i][j] == '#' ? '#' : r[toHash(i, j)] != long.MaxValue ? 'o' : 'x');
			Console.WriteLine();
		}
		Console.Out.Flush();
	}

	static string[] ReadEnclosedGrid(ref int height, ref int width, char c = '#', int delta = 1)
	{
		var h = height + 2 * delta;
		var w = width + 2 * delta;
		var cw = new string(c, w);
		var cd = new string(c, delta);

		var s = new string[h];
		for (int i = 0; i < delta; ++i) s[delta + height + i] = s[i] = cw;
		for (int i = 0; i < height; ++i) s[delta + i] = cd + Console.ReadLine() + cd;
		(height, width) = (h, w);
		return s;
	}

	static long[] Bfs(int n, Func<int, int[]> getNexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = costs[v] + 1;

			foreach (var nv in getNexts(v))
			{
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				if (nv == ev) return costs;
				q.Enqueue(nv);
			}
		}
		return costs;
	}
}
