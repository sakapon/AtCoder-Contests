using System;

namespace CoderLib8.Combinatorics
{
	// n 個の球を k 個の箱に入れる方法を列挙します。
	// 球を区別しません。
	public static class PartitionHelper
	{
		// 箱を区別する、0 個以上
		// 戻り値: 辞書順。
		public static void PartitionK0(int n, int k, Action<int[]> action)
		{
			var p = new int[k];
			DFS(0, n);

			void DFS(int i, int rem)
			{
				if (i == k - 1)
				{
					p[i] = rem;
					action(p); return;
				}

				for (int v = 0; v <= rem; ++v)
				{
					p[i] = v;
					DFS(i + 1, rem - v);
				}
			}
		}

		// 箱を区別する、1 個以上
		// 戻り値: 辞書順。
		public static void PartitionK1(int n, int k, Action<int[]> action)
		{
			var p = new int[k];
			DFS(0, n - k);

			void DFS(int i, int rem)
			{
				if (i == k - 1)
				{
					p[i] = rem + 1;
					action(p); return;
				}

				for (int v = 0; v <= rem; ++v)
				{
					p[i] = v + 1;
					DFS(i + 1, rem - v);
				}
			}
		}
	}
}
