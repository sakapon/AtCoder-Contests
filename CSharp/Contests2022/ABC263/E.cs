using System;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var a = Read();

		// dp[i] - 1 = (dp[i] + dp[i+1] + ... + dp[i+a[i]]) / (a[i] + 1)
		// a[i] * dp[i] = dp[i+1] + ... + dp[i+a[i]] + a[i] + 1
		var rsq = new RSQ(n);

		for (int i = n - 2; i >= 0; i--)
		{
			var v = rsq[i + 1, i + a[i] + 1] + a[i] + 1;
			v %= M;
			v *= MInv(a[i]);
			v %= M;
			rsq[i] = v;
		}
		return rsq[0];
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);
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
