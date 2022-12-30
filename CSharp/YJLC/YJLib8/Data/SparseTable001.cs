using System;
using System.Collections.Generic;

namespace YJLib8.Data.SparseTable001
{
	public static class Monoid
	{
		public static Monoid<int> Int32_Add { get; } = new Monoid<int>((x, y) => x + y);
		public static Monoid<long> Int64_Add { get; } = new Monoid<long>((x, y) => x + y);
		public static Monoid<int> Int32_Min { get; } = new Monoid<int>((x, y) => x <= y ? x : y, int.MaxValue);
		public static Monoid<int> Int32_Max { get; } = new Monoid<int>((x, y) => x >= y ? x : y, int.MinValue);

		public static Monoid<int> Int32_ArgMin(int[] a) => new Monoid<int>((x, y) => a[x] <= a[y] ? x : y);

		public static Monoid<int> Int32_Update { get; } = new Monoid<int>((x, y) => x != -1 ? x : y, -1);
	}

	public struct Monoid<T>
	{
		public Func<T, T, T> Op;
		public T Id;
		public Monoid(Func<T, T, T> op, T id = default(T)) { Op = op; Id = id; }
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class SparseTable<TValue>
	{
		readonly int n;
		// Power of 2
		readonly int n2 = 1;
		readonly Dictionary<(int, int), TValue> d = new Dictionary<(int, int), TValue>();

		public int Count => n;
		public Monoid<TValue> Merge { get; }

		public SparseTable(TValue[] items, Monoid<TValue> merge)
		{
			n = items.Length;
			while (n2 < n) n2 <<= 1;
			Merge = merge;

			for (int len = 2; len < n2; len <<= 1)
			{
				for (int c = len; c < n2; c += len)
				{
					for (int i = len >> 1; i < len; i++)
					{
						if (c <= n)
						{
							d[(c - i, c)] = i == 1 ? items[c - 1] : merge.Op(d[(c - i + 1, c)], items[c - i]);
						}
						if (c + i <= n)
						{
							d[(c, c + i)] = i == 1 ? items[c] : merge.Op(d[(c, c + i - 1)], items[c + i - 1]);
						}
					}
				}
			}
			for (int i = 1; i < n2; i++)
			{
				if (n2 <= n)
				{
					d[(n2 - i, n2)] = i == 1 ? items[n2 - 1] : merge.Op(d[(n2 - i + 1, n2)], items[n2 - i]);
				}
				if (i <= n)
				{
					d[(0, i)] = i == 1 ? items[0] : merge.Op(d[(0, i - 1)], items[i - 1]);
				}
			}
			if (n2 <= n)
			{
				d[(0, n2)] = n2 == 1 ? items[0] : merge.Op(d[(0, n2 - 1)], items[n2 - 1]);
			}
		}

		public TValue this[int index] => this[index, index + 1];
		public TValue this[int left, int right] => left < right ? GetRange(left, right) : Merge.Id;

		TValue GetRange(int left, int right)
		{
			var (l, r) = (left, right);
			if ((l, r) == (0, n2)) return d[(left, right)];

			for (int f = 1; l < r; f <<= 1)
			{
				if ((l & f) != 0) l += f;
				if ((r & f) != 0) r -= f;
			}
			if (l == left || r == right) return d[(left, right)];
			return Merge.Op(d[(left, r)], d[(l, right)]);
		}
	}
}
