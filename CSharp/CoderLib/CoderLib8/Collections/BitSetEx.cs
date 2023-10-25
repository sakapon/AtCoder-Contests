using System;
using System.Collections.Generic;
using BInt = System.UInt32;

namespace CoderLib8.Collections
{
	public static class BitSetEx
	{
		const int Size = 32;
		const BInt B1 = 1;

		public static bool Contains(this BInt x, int i) => (x & (1U << i)) != 0;
		public static BInt Add(this BInt x, int i) => x | (1U << i);
		public static BInt Remove(this BInt x, int i) => x & ~(1U << i);
		public static string ToBitString(this BInt x, int n = Size) => Convert.ToString(x, 2).PadLeft(n, '0');
		public static BInt ToBitSet32(this string s) => Convert.ToUInt32(s, 2);

		public static int Min(this BInt x, int n)
		{
			var i = 0;
			while (i < n && (x & (1U << i)) == 0) ++i;
			return i;
		}

		public static int Max(this BInt x, int n)
		{
			var i = n - 1;
			while (i >= 0 && (x & (1U << i)) == 0) --i;
			return i;
		}

		public static IEnumerable<BInt> NextSupersets(this BInt x, int n)
		{
			for (var f = 1U; f < (1U << n); f <<= 1)
				if ((x & f) == 0) yield return x | f;
		}

		public static IEnumerable<BInt> NextSubsets(this BInt x, int n)
		{
			for (var f = 1U << n - 1; f > 0; f >>= 1)
				if ((x & f) != 0) yield return x & ~f;
		}

		public static bool[] ToBitArray(this BInt x, int n)
		{
			var b = new bool[n];
			for (int i = 0; i < n; ++i)
				b[i] = (x & (1U << i)) != 0;
			return b;
		}

		public static BInt ToBitSet32(this bool[] b)
		{
			var x = 0U;
			for (int i = 0; i < b.Length; ++i)
				if (b[i]) x |= 1U << i;
			return x;
		}

		public static IEnumerable<int> ToElements(this BInt x, int n)
		{
			for (int i = 0; i < n; ++i)
				if ((x & (1U << i)) != 0) yield return i;
		}

		public static BInt ToBitSet32(this IEnumerable<int> s)
		{
			var x = 0U;
			foreach (var i in s)
				x |= 1U << i;
			return x;
		}
	}
}
