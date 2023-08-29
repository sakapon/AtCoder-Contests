using System;

namespace CoderLib8.Combinatorics
{
	public static class CombinationHelper
	{
		// [0, n) から r 個を選ぶ方法を列挙します。
		// 戻り値: 選ばれた r 個の要素。辞書順。
		public static void Combination(int n, int r, Action<int[]> action)
		{
			var p = new int[r];
			DFS(0, 0);

			void DFS(int v, int si)
			{
				if (v == r) { action(p); return; }

				for (int i = si; r - v <= n - i; ++i)
				{
					p[v] = i;
					DFS(v + 1, i + 1);
				}
			}
		}

		// [0, n) から r 個を選ぶ方法を列挙します。
		// 戻り値: n 個の各要素に対し、選択されたかどうか。
		public static void CombinationByBool(int n, int r, Action<bool[]> action)
		{
			var u = new bool[n];
			DFS(0, 0);

			void DFS(int v, int si)
			{
				if (v == r) { action(u); return; }

				for (int i = si; r - v <= n - i; ++i)
				{
					u[i] = true;
					DFS(v + 1, i + 1);
					u[i] = false;
				}
			}
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
	}
}
