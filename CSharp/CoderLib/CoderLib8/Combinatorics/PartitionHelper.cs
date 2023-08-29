using System;

namespace CoderLib8.Combinatorics
{
	// n 個の球を k 個の箱に入れる方法を列挙します。
	// 球を区別しません。
	public static class PartitionHelper
	{
		// 箱を区別する、0 個以上
		// 戻り値: 各グループにおける要素数。辞書順。
		public static void PartitionK0(int n, int k, Func<int[], bool> action)
		{
			var p = new int[k];
			DFS(0, n);

			bool DFS(int i, int rem)
			{
				if (i == k - 1)
				{
					p[i] = rem;
					return action(p);
				}

				for (int v = 0; v <= rem; ++v)
				{
					p[i] = v;
					if (DFS(i + 1, rem - v)) return true;
				}
				return false;
			}
		}

		// 箱を区別する、1 個以上
		// 戻り値: 各グループにおける要素数。辞書順。
		public static void PartitionK1(int n, int k, Func<int[], bool> action)
		{
			var p = new int[k];
			DFS(0, n - k);

			bool DFS(int i, int rem)
			{
				if (i == k - 1)
				{
					p[i] = rem + 1;
					return action(p);
				}

				for (int v = 0; v <= rem; ++v)
				{
					p[i] = v + 1;
					if (DFS(i + 1, rem - v)) return true;
				}
				return false;
			}
		}
	}
}
