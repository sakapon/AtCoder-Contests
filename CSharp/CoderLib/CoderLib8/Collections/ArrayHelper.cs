using System;

namespace CoderLib8.Collections
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/7/DPL/1
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/7/DPL/2
	static class ArrayHelper
	{
		const string Alphabets = "abcdefghijklmnopqrstuvwxyz";
		const string Numbers = "0123456789";

		//const long max = 1L << 60;
		//const long min = -1L << 60;
		const int max = 1 << 30;
		const int min = -1 << 30;

		static T[] NewArray1<T>(int n, T v = default) => Array.ConvertAll(new bool[n], _ => v);
		static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
		static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));

		static void SetMin1(long[] a, int i, long v) { if (a[i] > v) a[i] = v; }
		static void SetMin2(long[][] a, int i, int j, long v) { if (a[i][j] > v) a[i][j] = v; }
		static void SetMin3(long[][][] a, int i, int j, int k, long v) { if (a[i][j][k] > v) a[i][j][k] = v; }

		static void SetMax1(long[] a, int i, long v) { if (a[i] < v) a[i] = v; }
		static void SetMax2(long[][] a, int i, int j, long v) { if (a[i][j] < v) a[i][j] = v; }
		static void SetMax3(long[][][] a, int i, int j, int k, long v) { if (a[i][j][k] < v) a[i][j][k] = v; }

		static int[] Range(int start, int count)
		{
			var a = new int[count];
			for (var i = 0; i < count; ++i) a[i] = start + i;
			return a;
		}

		static int[] Range2(int l_in, int r_ex)
		{
			var a = new int[r_ex - l_in];
			for (var i = l_in; i < r_ex; ++i) a[i - l_in] = i;
			return a;
		}

		static (T, T) ToTuple2<T>(T[] a) => (a[0], a[1]);
		static (T, T, T) ToTuple3<T>(T[] a) => (a[0], a[1], a[2]);

		// 2次元配列に2次元インデックスでアクセスします。
		public static T GetByP<T>(this T[][] a, (int i, int j) p) => a[p.i][p.j];
		public static void SetByP<T>(this T[][] a, (int i, int j) p, T value) => a[p.i][p.j] = value;
		public static char GetByP(this string[] s, (int i, int j) p) => s[p.i][p.j];

		// 3次元配列に3次元インデックスでアクセスします。
		public static T GetByP<T>(this T[][][] a, (int i, int j, int k) p) => a[p.i][p.j][p.k];
		public static void SetByP<T>(this T[][][] a, (int i, int j, int k) p, T value) => a[p.i][p.j][p.k] = value;

		// 1次元インデックスでアクセスします。
		public static T GetBySeqIndex<T>(this T[][] a, int i, int n2) => a[i / n2][i % n2];
		public static void SetBySeqIndex<T>(this T[][] a, int i, int n2, T value) => a[i / n2][i % n2] = value;
	}

	static class ArrayHelperLab
	{
		static T[] NewArrayByFunc<T>(int n, Func<T> newItem) => Array.ConvertAll(new bool[n], _ => newItem());
		static T[] NewArray1<T>(int n, T v = default) => Array.ConvertAll(new bool[n], _ => v);
		static T[][] NewArray2<T>(int n1, int n2, T v = default) => NewArrayByFunc(n1, () => NewArray1(n2, v));
		static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => NewArrayByFunc(n1, () => NewArray2(n2, n3, v));
		static T[][][][] NewArray4<T>(int n1, int n2, int n3, int n4, T v = default) => NewArrayByFunc(n1, () => NewArray3(n2, n3, n4, v));
	}

	// 2次元配列に1次元インデックスでアクセスします。
	class SeqArray2<T>
	{
		int n1, n2;
		public T[][] a;
		public SeqArray2(int _n1, int _n2)
		{
			n1 = _n1;
			n2 = _n2;
			a = Array.ConvertAll(new bool[n1], _ => new T[n2]);
		}

		public T this[int i]
		{
			get => a[i / n2][i % n2];
			set => a[i / n2][i % n2] = value;
		}
		public T this[int i, int j]
		{
			get => a[i][j];
			set => a[i][j] = value;
		}
		public T this[(int i, int j) p]
		{
			get => a[p.i][p.j];
			set => a[p.i][p.j] = value;
		}
	}
}
