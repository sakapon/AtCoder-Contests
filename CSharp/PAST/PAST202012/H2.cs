using System;
using System.Collections.Generic;

class H2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		Point sv = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine().ToCharArray());
		EncloseGrid(ref h, ref w, ref s, '#');

		Func<Point, int> toHash = p => p.i * w + p.j;

		var r = Bfs(h * w, toHash, v =>
		{
			var (i, j) = v;
			var nvs = new List<Point>();
			char c;

			if ((c = s[i - 1][j]) == '.' || c == 'v') nvs.Add(new Point(i - 1, j));
			if ((c = s[i + 1][j]) == '.' || c == '^') nvs.Add(new Point(i + 1, j));
			if ((c = s[i][j - 1]) == '.' || c == '>') nvs.Add(new Point(i, j - 1));
			if ((c = s[i][j + 1]) == '.' || c == '<') nvs.Add(new Point(i, j + 1));

			return nvs.ToArray();
		}, sv, (-1, -1));

		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
			{
				if (s[i][j] == '#') continue;
				s[i][j] = r[toHash(new Point(i, j))] != long.MaxValue ? 'o' : 'x';
			}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int i = 1; i < h - 1; i++)
		{
			for (int j = 1; j < w - 1; j++)
				Console.Write(s[i][j]);
			Console.WriteLine();
		}
		Console.Out.Flush();
	}

	static void EncloseGrid<T>(ref int height, ref int width, ref T[][] a, T value, int delta = 1)
	{
		var h = height + 2 * delta;
		var w = width + 2 * delta;

		var t = Array.ConvertAll(new bool[h], _ => Array.ConvertAll(new bool[w], __ => value));
		for (int i = 0; i < height; ++i)
			for (int j = 0; j < width; ++j)
				t[delta + i][delta + j] = a[i][j];
		(height, width, a) = (h, w, t);
	}

	static long[] Bfs<TVertex>(int vertexesCount, Func<TVertex, int> toHash, Func<TVertex, TVertex[]> getNextVertexes, TVertex startVertex, TVertex endVertex)
	{
		var startVertexId = toHash(startVertex);
		var endVertexId = toHash(endVertex);

		var costs = Array.ConvertAll(new bool[vertexesCount], _ => long.MaxValue);
		var q = new Queue<TVertex>();
		costs[startVertexId] = 0;
		q.Enqueue(startVertex);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var vid = toHash(v);
			var nc = costs[vid] + 1;

			foreach (var nv in getNextVertexes(v))
			{
				var nvid = toHash(nv);
				if (costs[nvid] <= nc) continue;
				costs[nvid] = nc;
				if (nvid == endVertexId) return costs;
				q.Enqueue(nv);
			}
		}
		return costs;
	}

	public struct Point
	{
		public int i, j;
		public Point(int i, int j) { this.i = i; this.j = j; }
		public void Deconstruct(out int i, out int j) { i = this.i; j = this.j; }
		public static implicit operator Point((int i, int j) v) => new Point(v.i, v.j);
	}
}
