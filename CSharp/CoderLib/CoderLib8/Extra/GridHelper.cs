using System;
using System.Collections.Generic;
using CoderLib8.Values;

namespace CoderLib8.Extra
{
	// Test: https://atcoder.jp/contests/abc218/tasks/abc218_c
	public static class GridHelper
	{
		//int ToId(int i, int j) => i * w + j;
		//(int i, int j) FromId(int id) => (id / w, id % w);
		//int[] Nexts(int id) => new[] { id + 1, id - 1, id + w, id - w };

		// 負値を指定できます。
		public static void Enclose<T>(ref int height, ref int width, ref T[][] a, T value, int delta = 1)
		{
			var (h, w) = (height + 2 * delta, width + 2 * delta);
			var (li, ri) = (Math.Max(0, -delta), Math.Min(height, height + delta));
			var (lj, rj) = (Math.Max(0, -delta), Math.Min(width, width + delta));

			var t = Array.ConvertAll(new bool[h], _ => Array.ConvertAll(new bool[w], __ => value));
			for (int i = li; i < ri; ++i)
				for (int j = lj; j < rj; ++j)
					t[delta + i][delta + j] = a[i][j];
			(height, width, a) = (h, w, t);
		}

		// 負値を指定できます。
		public static void Enclose(ref int height, ref int width, ref string[] s, char c = '#', int delta = 1)
		{
			var (h, w) = (height + 2 * delta, width + 2 * delta);
			var (li, ri) = (Math.Max(0, -delta), Math.Min(height, height + delta));
			var cw = new string(c, w);
			var cd = new string(c, Math.Max(0, delta));

			var t = new string[h];
			for (int i = 0; i < delta; ++i) t[delta + height + i] = t[i] = cw;
			for (int i = li; i < ri; ++i) t[delta + i] = delta >= 0 ? cd + s[i] + cd : s[i][-delta..(width + delta)];
			(height, width, s) = (h, w, t);
		}

		public static T[][] Rotate180<T>(T[][] a)
		{
			if (a.Length == 0) return a;
			var (h, w) = (a.Length, a[0].Length);
			var r = Array.ConvertAll(new bool[h], _ => new T[w]);
			for (int i = 0; i < h; ++i)
				for (int j = 0; j < w; ++j)
					r[i][j] = a[h - 1 - i][w - 1 - j];
			return r;
		}

		public static T[][] RotateLeft<T>(T[][] a)
		{
			if (a.Length == 0) return a;
			var (h, w) = (a.Length, a[0].Length);
			var r = Array.ConvertAll(new bool[w], _ => new T[h]);
			for (int i = 0; i < w; ++i)
				for (int j = 0; j < h; ++j)
					r[i][j] = a[j][w - 1 - i];
			return r;
		}

		public static T[][] RotateRight<T>(T[][] a)
		{
			if (a.Length == 0) return a;
			var (h, w) = (a.Length, a[0].Length);
			var r = Array.ConvertAll(new bool[w], _ => new T[h]);
			for (int i = 0; i < w; ++i)
				for (int j = 0; j < h; ++j)
					r[i][j] = a[h - 1 - j][i];
			return r;
		}

		public static string[] Rotate180(string[] s)
		{
			if (s.Length == 0) return s;
			var h = s.Length;
			var r = new string[h];
			for (int i = 0; i < h; ++i)
			{
				var cs = s[h - 1 - i].ToCharArray();
				Array.Reverse(cs);
				r[i] = new string(cs);
			}
			return r;
		}

		public static string[] RotateLeft(string[] s)
		{
			if (s.Length == 0) return s;
			var (h, w) = (s.Length, s[0].Length);
			var r = new string[w];
			for (int i = 0; i < w; ++i)
			{
				var cs = new char[h];
				for (int j = 0; j < h; ++j)
					cs[j] = s[j][w - 1 - i];
				r[i] = new string(cs);
			}
			return r;
		}

		public static string[] RotateRight(string[] s)
		{
			if (s.Length == 0) return s;
			var (h, w) = (s.Length, s[0].Length);
			var r = new string[w];
			for (int i = 0; i < w; ++i)
			{
				var cs = new char[h];
				for (int j = 0; j < h; ++j)
					cs[j] = s[h - 1 - j][i];
				r[i] = new string(cs);
			}
			return r;
		}

		public static T[,] Transpose<T>(T[,] a)
		{
			var n = a.GetLength(0);
			var m = a.GetLength(1);
			var r = new T[m, n];
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < m; ++j)
					r[j, i] = a[i, j];
			return r;
		}

		public static T[][] Transpose<T>(T[][] a)
		{
			var n = a.Length;
			var m = a[0].Length;
			var r = Array.ConvertAll(new bool[m], _ => new T[n]);
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < m; ++j)
					r[j][i] = a[i][j];
			return r;
		}

		public static string[] Transpose(string[] s)
		{
			var n = s.Length;
			var m = s[0].Length;
			var r = Array.ConvertAll(new bool[m], _ => new char[n]);
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < m; ++j)
					r[j][i] = s[i][j];
			return Array.ConvertAll(r, l => new string(l));
		}
	}

	public static class GridHelper2
	{
		public static string[] TrimTop(this string[] s, int delta) => s[delta..];
		public static string[] TrimBottom(this string[] s, int delta) => s[..^delta];
		public static string[] TrimLeft(this string[] s, int delta) => Array.ConvertAll(s, t => t[delta..]);
		public static string[] TrimRight(this string[] s, int delta) => Array.ConvertAll(s, t => t[..^delta]);

		public static string[] Trim(this string[] s, char c = '.')
		{
			if (s.Length == 0) return s;

			var (d1, d2) = (0, 0);
			var space = new string(c, s[0].Length);
			while (d1 < s.Length && s[d1] == space) ++d1;
			if (d1 == s.Length) return new string[0];
			while (s[^(d2 + 1)] == space) ++d2;
			s = s[d1..^d2];

			(d1, d2) = (0, 0);
			while (Array.TrueForAll(s, t => t[d1] == c)) ++d1;
			while (Array.TrueForAll(s, t => t[^(d2 + 1)] == c)) ++d2;
			return Array.ConvertAll(s, t => t[d1..^d2]);
		}
	}

	public static class GridHelper3
	{
		// 座標リスト表現
		public static (int i, int j)[] ToPoints(this string[] s, char c = '#')
		{
			if (s.Length == 0) return new (int, int)[0];
			var (h, w) = (s.Length, s[0].Length);
			var l = new List<(int, int)>();
			for (int i = 0; i < h; ++i)
				for (int j = 0; j < w; ++j)
					if (s[i][j] == c)
						l.Add((i, j));
			return l.ToArray();
		}

		public static (int i, int j) Rotate90(this (int i, int j) p) => (-p.j, p.i);
		public static (int i, int j) Rotate180(this (int i, int j) p) => (-p.i, -p.j);
		public static (int i, int j) Rotate270(this (int i, int j) p) => (p.j, -p.i);

		public static (int i, int j) Rotate90(this (int i, int j) p, int h, int w) => (w - 1 - p.j, p.i);
		public static (int i, int j) Rotate180(this (int i, int j) p, int h, int w) => (h - 1 - p.i, w - 1 - p.j);
		public static (int i, int j) Rotate270(this (int i, int j) p, int h, int w) => (p.j, h - 1 - p.i);

		// 左上から並べるには、ソートします。
		public static (int i, int j)[] Rotate90(this (int i, int j)[] ps) => Array.ConvertAll(ps, Rotate90);
		public static (int i, int j)[] Rotate180(this (int i, int j)[] ps) => Array.ConvertAll(ps, Rotate180);
		public static (int i, int j)[] Rotate270(this (int i, int j)[] ps) => Array.ConvertAll(ps, Rotate270);
	}

	public static class GridHelper4
	{
		// 座標リスト表現
		public static IntV[] ToPointsV(this string[] s, char c = '#')
		{
			if (s.Length == 0) return new IntV[0];
			var (h, w) = (s.Length, s[0].Length);
			var l = new List<IntV>();
			for (int i = 0; i < h; ++i)
				for (int j = 0; j < w; ++j)
					if (s[i][j] == c)
						l.Add(new IntV(i, j));
			return l.ToArray();
		}

		// 左上から並べるには、ソートします。
		public static IntV[] Rotate90(this IntV[] ps) => Array.ConvertAll(ps, p => p.Rotate90());
		public static IntV[] Rotate180(this IntV[] ps) => Array.ConvertAll(ps, p => p.Rotate180());
		public static IntV[] Rotate270(this IntV[] ps) => Array.ConvertAll(ps, p => p.Rotate270());
	}
}
