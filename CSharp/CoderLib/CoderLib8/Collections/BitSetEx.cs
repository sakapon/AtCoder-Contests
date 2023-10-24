using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	public static class BitSet32Ex
	{
		const int Size = 32;
		public static bool Contains(this uint x, int i) => (x & (1U << i)) != 0;
		public static uint Add(this uint x, int i) => x | (1U << i);
		public static uint Remove(this uint x, int i) => x & ~(1U << i);
		public static string ToBitString(this uint x, int n = Size) => Convert.ToString(x, 2).PadLeft(n, '0');
		public static uint ToBitSet32(this string s) => Convert.ToUInt32(s, 2);

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

		public static bool[] ToBitArray(this uint x, int n)
		{
			var b = new bool[n];
			for (int i = 0; i < n; ++i)
				b[i] = (x & (1U << i)) != 0;
			return b;
		}

		public static uint ToBitSet32(this bool[] b)
		{
			var x = 0U;
			for (int i = 0; i < b.Length; ++i)
				if (b[i]) x |= 1U << i;
			return x;
		}

		public static IEnumerable<int> ToElements(this uint x, int n)
		{
			for (int i = 0; i < n; ++i)
				if ((x & (1U << i)) != 0) yield return i;
		}

		public static uint ToBitSet32(this IEnumerable<int> s)
		{
			var x = 0U;
			foreach (var i in s)
				x |= 1U << i;
			return x;
		}
	}
}
