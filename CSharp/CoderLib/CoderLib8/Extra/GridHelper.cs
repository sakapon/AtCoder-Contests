using System;
using System.Collections.Generic;

namespace CoderLib8.Extra
{
	public static class GridHelper
	{
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
			var (h, w) = (a.Length, a[0].Length);
			var r = Array.ConvertAll(new bool[h], _ => new T[w]);
			for (int i = 0; i < h; ++i)
				for (int j = 0; j < w; ++j)
					r[i][j] = a[h - 1 - i][w - 1 - j];
			return r;
		}

		public static T[][] RotateLeft<T>(T[][] a)
		{
			var (h, w) = (a.Length, a[0].Length);
			var r = Array.ConvertAll(new bool[w], _ => new T[h]);
			for (int i = 0; i < w; ++i)
				for (int j = 0; j < h; ++j)
					r[i][j] = a[j][w - 1 - i];
			return r;
		}

		public static T[][] RotateRight<T>(T[][] a)
		{
			var (h, w) = (a.Length, a[0].Length);
			var r = Array.ConvertAll(new bool[w], _ => new T[h]);
			for (int i = 0; i < w; ++i)
				for (int j = 0; j < h; ++j)
					r[i][j] = a[h - 1 - j][i];
			return r;
		}

		public static string[] Rotate180(string[] s)
		{
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
	}
}
