using System;

class BB1
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		var n = h[0];

		var st = new BIT(n);

		for (int k = 0; k < h[1]; k++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Add(q[1], q[2]);
			else
				Console.WriteLine(st.Sum(q[1], q[2] + 1));
		}
		Console.Out.Flush();
	}
}

// 外見上は 1-indexed, 1 <= i <= n
// 内部では 1-indexed, 1 <= i <= n2
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
		get { return Sum(i) - Sum(i - 1); }
		set { Add(i, value - this[i]); }
	}

	public void Add(int i, long v)
	{
		for (; i <= n2; i += i & -i) a[i] += v;
	}

	public long Sum(int l_in, int r_ex) => Sum(r_ex - 1) - Sum(l_in - 1);
	public long Sum(int r_in)
	{
		var r = 0L;
		for (var i = r_in; i > 0; i -= i & -i) r += a[i];
		return r;
	}
}
