using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class BitSet : IEnumerable<bool>
	{
		readonly int n;
		readonly ulong[] a;

		public BitSet(int count)
		{
			n = count;
			a = new ulong[(n >> 6) + 1];
		}

		public int Count => n;
		public bool this[int i]
		{
			get => (a[i >> 6] & (1UL << i)) != 0;
			set
			{
				if (value)
					a[i >> 6] |= 1UL << i;
				else
					a[i >> 6] &= ~(1UL << i);
			}
		}

		public static BitSet operator &(BitSet v1, BitSet v2) => v1.And(v2);
		public static BitSet operator |(BitSet v1, BitSet v2) => v1.Or(v2);
		public static BitSet operator ^(BitSet v1, BitSet v2) => v1.Xor(v2);

		public BitSet And(BitSet other)
		{
			var set = new BitSet(n);
			for (int i = 0; i < a.Length; ++i) set.a[i] = a[i] & other.a[i];
			return set;
		}

		public BitSet Or(BitSet other)
		{
			var set = new BitSet(n);
			for (int i = 0; i < a.Length; ++i) set.a[i] = a[i] | other.a[i];
			return set;
		}

		public BitSet Xor(BitSet other)
		{
			var set = new BitSet(n);
			for (int i = 0; i < a.Length; ++i) set.a[i] = a[i] ^ other.a[i];
			return set;
		}

		// .NET Standard 2.1 には BitOperations クラスが含まれていません。
		public int PopCount()
		{
			var r = 0;
			for (int i = 0; i < a.Length; ++i) r += PopCount(a[i]);
			return r;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<bool> GetEnumerator() { for (var i = 0; i < n; ++i) yield return this[i]; }

		#region PopCount
		const uint F16 = (1 << 16) - 1;
		static readonly int[] Pop16 = InitPop16();
		static int[] InitPop16()
		{
			var a = new int[1 << 16];
			for (int f = 1; f < a.Length; f <<= 1)
				for (int i = 0; i < f; ++i)
					a[f | i] = a[i] + 1;
			return a;
		}
		public static int PopCount(uint x) => Pop16[x & F16] + Pop16[x >> 16 & F16];
		public static int PopCount(ulong x) => Pop16[x & F16] + Pop16[x >> 16 & F16] + Pop16[x >> 32 & F16] + Pop16[x >> 48 & F16];
		#endregion
	}
}
