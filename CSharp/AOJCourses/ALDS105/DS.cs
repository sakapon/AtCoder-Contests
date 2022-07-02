using System;
using System.Linq;

class DS
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var a = Read();

		var p = Enumerable.Range(0, n).OrderBy(i => a[i]).ToArray();
		return InversionNumberFrom0(n, p);
	}

	static long InversionNumberFrom0(int max_ex, int[] a)
	{
		var r = 0L;
		var rsq = new RSQ(max_ex);
		foreach (var v in a)
		{
			r += rsq[v + 1, max_ex];
			rsq.Add(v, 1);
		}
		return r;
	}
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
