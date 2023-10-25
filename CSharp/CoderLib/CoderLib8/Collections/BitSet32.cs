using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	public struct BitSet32
	{
		public uint Value;

		public bool Contains(int index) => (Value & (1U << index)) != 0;
		public bool this[int index]
		{
			get => (Value & (1U << index)) != 0;
			set
			{
				if (value) Value |= 1U << index;
				else Value &= ~(1U << index);
			}
		}

		public BitSet32(uint value) => Value = value;
		public override string ToString() => Convert.ToString(Value, 2);
		public static BitSet32 Parse(string s) => Convert.ToUInt32(s, 2);

		public static implicit operator BitSet32(uint v) => new BitSet32(v);
		public static explicit operator uint(BitSet32 v) => v.Value;

		public static BitSet32 operator ++(BitSet32 v) => v.Value + 1;
		public static BitSet32 operator --(BitSet32 v) => v.Value - 1;

		public static BitSet32 operator &(BitSet32 v1, BitSet32 v2) => v1.Value & v2.Value;
		public static BitSet32 operator |(BitSet32 v1, BitSet32 v2) => v1.Value | v2.Value;
		public static BitSet32 operator ^(BitSet32 v1, BitSet32 v2) => v1.Value ^ v2.Value;

		public BitSet32 Add(int index) => Value | (1U << index);
		public BitSet32 Remove(int index) => Value & ~(1U << index);

		public IEnumerable<BitSet32> NextSuperSets(int n)
		{
			for (int i = 0; i < n; ++i)
				if ((Value & (1U << i)) == 0) yield return Value | (1U << i);
		}

		public IEnumerable<BitSet32> NextSubSets(int n)
		{
			for (int i = n - 1; i >= 0; --i)
				if ((Value & (1U << i)) != 0) yield return Value & ~(1U << i);
		}
	}
}
