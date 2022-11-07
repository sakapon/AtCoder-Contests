using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoderLib8.Collections.Dynamics.Int;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var sb = new StringBuilder();

		var rsi = new IntSegmentRangeSum(200000 + 1);
		var rsv = new IntSegmentRangeSum(200000 + 1);
		MInt sum = 0;

		for (int i = 1; i <= n; i++)
		{
			var x = a[i - 1];

			sum += x * (2 * rsi[0, x + 1] + 1);
			sum += 2 * rsv[x + 1, 200000 + 1];

			sb.AppendLine((sum / ((long)i * i)).ToString());

			rsi[x]++;
			rsv[x] += x;
		}

		Console.Write(sb);
	}
}

struct MInt
{
	const long M = 998244353;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }
	public override string ToString() => $"{V}";
	public static implicit operator MInt(long v) => new MInt(v);

	public static MInt operator -(MInt x) => -x.V;
	public static MInt operator +(MInt x, MInt y) => x.V + y.V;
	public static MInt operator -(MInt x, MInt y) => x.V - y.V;
	public static MInt operator *(MInt x, MInt y) => x.V * y.V;
	public static MInt operator /(MInt x, MInt y) => x.V * y.Inv().V;

	public static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	public MInt Pow(long i) => MPow(V, i);
	public MInt Inv() => MPow(V, M - 2);
}

namespace CoderLib8.Collections.Dynamics.Int
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
