using System;

namespace CoderLib8.Combinatorics
{
	public static class CombinationHelper
	{
		// [0, n) から r 個を選ぶ方法を列挙します。
		// 戻り値: 選ばれた r 個の要素。辞書順。
		public static void Combination(int n, int r, Func<int[], bool> action)
		{
			var p = new int[r];
			DFS(0, 0);

			bool DFS(int v, int si)
			{
				if (v == r) return action(p);

				for (int i = si; r - v <= n - i; ++i)
				{
					p[v] = i;
					if (DFS(v + 1, i + 1)) return true;
				}
				return false;
			}
		}

		// [0, n) から r 個を選ぶ方法を列挙します。
		// 戻り値: n 個の各要素に対し、選択されたかどうか。
		public static void CombinationForBoxes(int n, int r, Func<bool[], bool> action)
		{
			var u = new bool[n];
			DFS(0, 0);

			bool DFS(int v, int si)
			{
				if (v == r) return action(u);

				for (int i = si; r - v <= n - i; ++i)
				{
					u[i] = true;
					if (DFS(v + 1, i + 1)) return true;
					u[i] = false;
				}
				return false;
			}
		}

		// [0, n) から r 個を選ぶ方法を列挙します。
		// 戻り値: n 個の各要素に対し、選択されたかどうか。
		// 集合を bit で表現
		public static void CombinationForBoxes(int n, int r, Func<long, bool> action)
		{
			var x = (1L << r) - 1;
			do action(x);
			while (NextCombination(n, ref x));
		}

		public static bool NextCombination(int n, ref long x)
		{
			var l = -x & x;
			x += l;
			if ((x & ((1L << n) - 1)) == 0) return false;
			x |= ((-x & x) / l >> 1) - 1;
			return true;
		}

		// n 個の要素から r 個を選ぶ方法を列挙します。
		// 戻り値: 選ばれた r 個の要素。辞書順。
		public static void Combination<T>(T[] a, int r, Func<T[], bool> action)
		{
			var n = a.Length;
			var p = new T[r];
			DFS(0, 0);

			bool DFS(int v, int si)
			{
				if (v == r) return action(p);

				for (int i = si; r - v <= n - i; ++i)
				{
					p[v] = a[i];
					if (DFS(v + 1, i + 1)) return true;
				}
				return false;
			}
		}

		// n 個の要素から r 個を選ぶ方法を列挙します。
		public static void Combination<T>(T[] a, int r, Func<Result<T>, bool> action)
		{
			var n = a.Length;
			var o = new Result<T> { Values = new T[r], Selected = new bool[n] };
			DFS(0, 0);

			bool DFS(int v, int si)
			{
				if (v == r) return action(o);

				for (int i = si; r - v <= n - i; ++i)
				{
					o.Values[v] = a[i];
					o.Selected[i] = true;
					if (DFS(v + 1, i + 1)) return true;
					o.Selected[i] = false;
				}
				return false;
			}
		}

		public class Result<T>
		{
			public T[] Values;
			public bool[] Selected;
		}
	}
}
