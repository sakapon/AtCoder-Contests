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
			while (i >= 0 && p[i] >= p[i + 1]) i--;
			if (i == -1) return false;

			// p[i] < p[j] を満たす最大の j
			var j = i + 1;
			while (j + 1 < n && p[i] < p[j + 1]) j++;

			(p[i], p[j]) = (p[j], p[i]);
			Array.Reverse(p, i + 1, n - i - 1);
			return true;
		}

		// 値は何でもかまいません。重複可能。
		public static bool PreviousPermutation(int[] p)
		{
			var n = p.Length;

			// p[i] > p[i + 1] を満たす最大の i
			var i = n - 2;
			while (i >= 0 && p[i] <= p[i + 1]) i--;
			if (i == -1) return false;

			// p[i] > p[j] を満たす最大の j
			var j = i + 1;
			while (j + 1 < n && p[i] > p[j + 1]) j++;

			(p[i], p[j]) = (p[j], p[i]);
			Array.Reverse(p, i + 1, n - i - 1);
			return true;
		}
	}
}
