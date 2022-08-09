using System;
using System.Linq;
using System.Text;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, qc) = Read2();
		var a = ReadL();
		var sb = new StringBuilder();

		var rsq0 = new RSQ(n, a);
		var rsq1 = new RSQ(n, a.Select((v, i) => v * i % M).ToArray());
		var rsq2 = new RSQ(n, a.Select((v, i) => v * i % M * i % M).ToArray());

		while (qc-- > 0)
		{
			var q = Read();
			var x = q[1] - 1;
			if (q[0] == 1)
			{
				long v = q[2];
				rsq0[x] = v;
				rsq1[x] = v * x % M;
				rsq2[x] = rsq1[x] * x % M;
			}
			else
			{
				var r = rsq0[0, x + 1] % M * (x + 1) % M * (x + 2);
				r -= rsq1[0, x + 1] % M * (2 * x + 3);
				r += rsq2[0, x + 1];
				r = MInt(r);
				sb.Append(r * MHalf % M).AppendLine();
			}
		}
		Console.Write(sb);
	}

	const long M = 998244353;
	const long MHalf = (M + 1) / 2;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}

public class RSQ
{
	int n = 1;
	public int Count => n;
	long[] a;

	public RSQ(int count, long[] a0 = null)
	{
		while (n < count) n <<= 1;
		a = new long[n << 1];
		if (a0 != null)
		{
			Array.Copy(a0, 0, a, n, a0.Length);
			for (int i = n - 1; i > 0; --i) a[i] = a[i << 1] + a[(i << 1) | 1];
		}
	}

	public long this[int i]
	{
		get => a[n | i];
		set => Add(i, value - a[n | i]);
	}
	public void Add(int i, long d) { for (i |= n; i > 0; i >>= 1) a[i] += d; }

	public long this[int l, int r]
	{
		get
		{
			var s = 0L;
			for (l += n, r += n; l < r; l >>= 1, r >>= 1)
			{
				if ((l & 1) != 0) s += a[l++];
				if ((r & 1) != 0) s += a[--r];
			}
			return s;
		}
	}
}
