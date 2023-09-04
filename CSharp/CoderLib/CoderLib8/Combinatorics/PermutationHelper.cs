using System;

namespace CoderLib8.Combinatorics
{
	public static class PermutationHelper
	{
		// 値は何でもかまいません。重複可能。
		public static bool NextPermutation(int[] p)
		{
			var n = p.Length;

			// p[i] < p[i + 1] を満たす最大の i
			var i = n - 2;
			while (i >= 0 && p[i] >= p[i + 1]) --i;
			if (i < 0) return false;

			// p[i] < p[j] を満たす最大の j
			var j = i + 1;
			while (j + 1 < n && p[i] < p[j + 1]) ++j;

			(p[i], p[j]) = (p[j], p[i]);
			Array.Reverse(p, i + 1, n - i - 1);
			return true;
		}

		// Test: https://atcoder.jp/contests/abc276/tasks/abc276_c
		// 値は何でもかまいません。重複可能。
		public static bool PreviousPermutation(int[] p)
		{
			var n = p.Length;

			// p[i] > p[i + 1] を満たす最大の i
			var i = n - 2;
			while (i >= 0 && p[i] <= p[i + 1]) --i;
			if (i < 0) return false;

			// p[i] > p[j] を満たす最大の j
			var j = i + 1;
			while (j + 1 < n && p[i] > p[j + 1]) ++j;

			(p[i], p[j]) = (p[j], p[i]);
			Array.Reverse(p, i + 1, n - i - 1);
			return true;
		}

		// [0, n) から n 個を選ぶ方法を列挙します。
		// 戻り値: 選ばれた r 個の要素。辞書順。
		public static void Permutation(int n, Func<int[], bool> action)
		{
			var p = new int[n];
			for (int i = 0; i < n; ++i) p[i] = i;
			while (!action(p) && NextPermutation(p)) ;
		}

		// [0, n) から r 個を選ぶ方法を列挙します。
		// 戻り値: n 個の各要素に対し、選択された順序。
		public static void PermutationForBoxes(int n, int r, Func<int[], bool> action)
		{
			var p = new int[n];
			Array.Fill(p, r);
			for (int i = 0; i < r; ++i) p[i] = i;
			while (!action(p) && NextPermutation(p)) ;
		}

		// [0, n) から r 個を選ぶ方法を列挙します。
		// 戻り値: 選ばれた r 個の要素。辞書順。
		public static void Permutation(int n, int r, Func<int[], bool> action)
		{
			var p = new int[r];
			var b = new int[n];
			Array.Fill(b, -1);
			DFS(0);

			bool DFS(int v)
			{
				if (v == r) return action(p);

				for (int i = 0; i < n; ++i)
				{
					if (b[i] != -1) continue;
					p[v] = i;
					b[i] = v;
					if (DFS(v + 1)) return true;
					b[i] = -1;
				}
				return false;
			}
		}

		// n 個の要素から r 個を選ぶ方法を列挙します。
		// 戻り値: 選ばれた r 個の要素。辞書順。
		public static void Permutation<T>(T[] a, int r, Func<T[], bool> action)
		{
			var n = a.Length;
			var p = new T[r];
			var b = new int[n];
			Array.Fill(b, -1);
			DFS(0);

			bool DFS(int v)
			{
				if (v == r) return action(p);

				for (int i = 0; i < n; ++i)
				{
					if (b[i] != -1) continue;
					p[v] = a[i];
					b[i] = v;
					if (DFS(v + 1)) return true;
					b[i] = -1;
				}
				return false;
			}
		}
	}
}
