using System;

namespace CoderLib8.Combinatorics
{
	// n 個の球を k 個の箱に入れる方法を列挙します。
	// 球を区別しません。
	public static class PartitionHelper
	{
		// 箱を区別する、0 個以上
		// 戻り値: 各グループにおける要素数。辞書順。
		public static void PartitionLabeled0(int n, int k, Func<int[], bool> action)
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

				for (int c = 0; c <= rem; ++c)
				{
					p[i] = c;
					if (DFS(i + 1, rem - c)) return true;
				}
				return false;
			}
		}

		// 箱を区別する、1 個以上
		// 戻り値: 各グループにおける要素数。辞書順。
		public static void PartitionLabeled1(int n, int k, Func<int[], bool> action)
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

				for (int c = 0; c <= rem; ++c)
				{
					p[i] = c + 1;
					if (DFS(i + 1, rem - c)) return true;
				}
				return false;
			}
		}

		// 箱を区別しない、0 個以上
		// 戻り値: 各グループにおける要素数。逆辞書順。
		public static void Partition0(int n, int k, Func<int[], bool> action)
		{
			var p = new int[k];
			DFS(0, n, n);

			bool DFS(int i, int rem, int max)
			{
				if (rem == 0) return action(p);

				for (int c = rem <= max ? rem : max; rem <= c * (k - i); --c)
				{
					p[i] = c;
					if (DFS(i + 1, rem - c, c)) return true;
				}
				p[i] = 0;
				return false;
			}
		}

		// 箱を区別しない、1 個以上
		// 戻り値: 各グループにおける要素数。逆辞書順。
		public static void Partition1(int n, int k, Func<int[], bool> action)
		{
			var p = new int[k];
			Array.Fill(p, 1);
			DFS(0, n - k, n - k);

			bool DFS(int i, int rem, int max)
			{
				if (rem == 0) return action(p);

				for (int c = rem <= max ? rem : max; rem <= c * (k - i); --c)
				{
					p[i] = c + 1;
					if (DFS(i + 1, rem - c, c)) return true;
				}
				p[i] = 1;
				return false;
			}
		}
	}
}
