using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	public static class BitSet32Ex
	{
		public static bool Contains(this uint x, int i) => (x & (1U << i)) != 0;
		public static uint Add(this uint x, int i) => x | (1U << i);
		public static uint Remove(this uint x, int i) => x & ~(1U << i);
		public static string ToBitString(this uint x, int n = 0) => Convert.ToString(x, 2).PadLeft(n, '0');
		public static uint ToUInt32(this string s) => Convert.ToUInt32(s, 2);

		public static int Min(this uint x, int n)
		{
			var i = 0;
			while (i < n && (x & (1U << i)) == 0) ++i;
			return i;
		}

		public static int Max(this uint x, int n)
		{
			var i = n - 1;
			while (i >= 0 && (x & (1U << i)) == 0) --i;
			return i;
		}

		public static IEnumerable<uint> NextSupersets(this uint x, int n)
		{
			for (var f = 1U; f < (1U << n); f <<= 1)
				if ((x & f) == 0) yield return x | f;
		}

		public static IEnumerable<uint> NextSubsets(this uint x, int n)
		{
			for (var f = 1U << n - 1; f > 0; f >>= 1)
				if ((x & f) != 0) yield return x & ~f;
		}
	}
}
