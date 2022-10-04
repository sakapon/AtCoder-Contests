using System;
using System.Collections.Generic;
using System.Linq;
using DSLab.Collections.Dynamics.Int;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var c = Read();
		var x = Read();

		var cmap = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int i = 0; i < n; i++)
		{
			cmap[c[i]].Add(x[i]);
		}

		var r = InversionNumber(x);
		foreach (var l in cmap)
		{
			if (l.Count == 0) continue;
			r -= InversionNumber(l.ToArray());
		}
		return r;
	}

	public static long InversionNumber(int aMax_ex, int[] a)
	{
		var r = 0L;
		var rsq = new IntSegmentRangeSum(aMax_ex);
		foreach (var v in a)
		{
			r += rsq[v + 1, aMax_ex];
			rsq.Add(v);
		}
		return r;
	}

	public static long InversionNumber<T>(T[] a)
	{
		// 値が重複しない場合、次の方法で高速化できます。
		//var p = Enumerable.Range(0, a.Length).ToArray();
		//Array.Sort(a, p);
		var p = Enumerable.Range(0, a.Length).OrderBy(i => a[i]).ToArray();
		return InversionNumber(a.Length, p);
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
