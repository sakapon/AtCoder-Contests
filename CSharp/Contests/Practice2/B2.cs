using System;
using System.Linq;

class B2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		var n = h[0];
		var a = Read();

		var st = new BIT(n);
		for (int i = 0; i < n; i++)
			st.Add(i + 1, a[i]);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Add(q[1] + 1, q[2]);
			else
				Console.WriteLine(st.Subsum(q[1] + 1, q[2] + 1));
		}
		Console.Out.Flush();
	}
}

class BIT
{
	// Power of 2
	int n2 = 1;
	long[] a;

	public BIT(int n)
	{
		while (n2 < n) n2 <<= 1;
		a = new long[n2 + 1];
	}

	public long this[int i]
	{
		get => Subsum(i) - Subsum(i - 1);
		set => Add(i, value - this[i]);
	}

	public void Add(int i, long v)
	{
		for (; i <= n2; i += i & -i) a[i] += v;
	}

	public long Subsum(int minIn, int maxEx) => Subsum(maxEx - 1) - Subsum(minIn - 1);
	public long Subsum(int maxIn)
	{
		var r = 0L;
		for (var i = maxIn; i > 0; i -= i & -i) r += a[i];
		return r;
	}
}
