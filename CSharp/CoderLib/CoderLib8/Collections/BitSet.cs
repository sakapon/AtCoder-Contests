using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class BitSet : IEnumerable<bool>
	{
		int n;
		ulong[] a;

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

		public BitSet And(BitSet other)
		{
			var set = new BitSet(n);
			for (int i = 0; i < a.Length; ++i) set.a[i] = a[i] & other.a[i];
			return set;
		}

		// .NET Standard 2.1 には BitOperations クラスが含まれていません。
		//public int PopCount()
		//{
		//	var r = 0;
		//	for (int i = 0; i < a.Length; ++i) r += BitOperations.PopCount(a[i]);
		//	return r;
		//}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<bool> GetEnumerator() { for (var i = 0; i < n; ++i) yield return this[i]; }
	}
}
