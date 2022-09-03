using System;
using System.Collections.Generic;
using System.Linq;
using DSLab.Collections.Dynamics.Int;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read().Prepend(0).ToArray();
		var b = Read().Prepend(0).ToArray();

		var aInv = ToInverseMap(a, n);
		for (int i = 0; i < b.Length; i++)
		{
			b[i] = aInv[b[i]];
		}

		var rsq = new IntSegmentRangeSum(n + 1);
		var c = 0L;
		foreach (var v in b)
		{
			c += rsq[v + 1, n + 1];
			rsq.Add(v);
		}
		if ((c & 1) != 0) return -1;

		var c2 = c >> 1;
		for (int i = 0; i <= n; i++)
		{
			c -= rsq[0, b[i]];
			rsq.Add(b[i], -1);

			if (c <= c2)
			{
				Array.Sort(b, i + 1, n - i);
				for (int j = i; c < c2; j++, c++)
				{
					(b[j], b[j + 1]) = (b[j + 1], b[j]);
				}
				break;
			}
		}

		for (int i = 0; i < b.Length; i++)
		{
			b[i] = a[b[i]];
		}
		return string.Join(" ", b[1..]);
	}

	public static int[] ToInverseMap(int[] a, int max)
	{
		var d = Array.ConvertAll(new bool[max + 1], _ => -1);
		for (int i = 0; i < a.Length; ++i) d[a[i]] = i;
		return d;
	}
}

namespace DSLab.Collections.Dynamics.Int
{
	public class IntSegmentRangeSum
	{
		readonly int n = 1;
		readonly long[] c;

		public IntSegmentRangeSum(int itemsCount, long[] counts = null)
		{
			while (n < itemsCount) n <<= 1;
			c = new long[n << 1];
			if (counts != null)
			{
				Array.Copy(counts, 0, c, n, counts.Length);
				for (int i = n - 1; i > 0; --i) c[i] = c[i << 1] + c[(i << 1) | 1];
			}
		}

		public int ItemsCount => n;
		public long Sum => c[1];

		public long this[int i]
		{
			get => c[n | i];
			set => Add(i, value - c[n | i]);
		}

		public long this[int l, int r]
		{
			get
			{
				var s = 0L;
				for (l += n, r += n; l < r; l >>= 1, r >>= 1)
				{
					if ((l & 1) != 0) s += c[l++];
					if ((r & 1) != 0) s += c[--r];
				}
				return s;
			}
		}

		public void Add(int i, long d = 1) { for (i |= n; i > 0; i >>= 1) c[i] += d; }
	}
}
