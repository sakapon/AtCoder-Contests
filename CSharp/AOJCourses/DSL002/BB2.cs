using System;
using System.Collections.Generic;
using System.Linq;

class BB2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];

		var st = new BIT2(n);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Add(q[1], q[2]);
			else
				r.Add(st.Subsum(q[1], q[2] + 1));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}

// 外見上は 1-indexed, 1 <= i <= n
// 内部では 1-indexed, 1 <= i <= n
class BIT2
{
	int n;
	long[] a;

	// 配列の長さは n+1 で十分。
	public BIT2(int _n)
	{
		n = _n;
		a = new long[n + 1];
	}

	public long this[int i]
	{
		get { return Subsum(i) - Subsum(i - 1); }
		set { Add(i, value - this[i]); }
	}

	public void Add(int i, long v)
	{
		for (; i <= n; i += i & -i) a[i] += v;
	}

	public long Subsum(int minIn, int maxEx) => Subsum(maxEx - 1) - Subsum(minIn - 1);
	public long Subsum(int maxIn)
	{
		var r = 0L;
		for (var i = maxIn; i > 0; i -= i & -i) r += a[i];
		return r;
	}
}
