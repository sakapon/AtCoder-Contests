using System;

class BS
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, qc) = Read2();
		var a = ReadL();

		var st = new RSQ(n, a);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var q = Read();
			if (q[0] == 0)
				st.Add(q[1], q[2]);
			else
				Console.WriteLine(st[q[1], q[2]]);
		}
		Console.Out.Flush();
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
			for (l |= n, r += n; l < r; l >>= 1, r >>= 1)
			{
				if ((l & 1) != 0) s += a[l++];
				if ((r & 1) != 0) s += a[--r];
			}
			return s;
		}
	}
}
