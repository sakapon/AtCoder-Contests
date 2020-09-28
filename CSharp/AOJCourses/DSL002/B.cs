using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];

		var st = new ST_Subsum(n + 1);

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

class ST
{
	// Power of 2
	protected int n2 = 1;
	public long[] a;

	public ST(int n)
	{
		while (n2 < n) n2 <<= 1;
		a = new long[n2 <<= 1];
	}

	public void InitAllLevels(long v) { for (int i = 0; i < a.Length; ++i) a[i] = v; }

	public virtual long this[int i] => a[(n2 >> 1) + i];

	// 階層の降順
	public void ForLevels(int i, Action<int> action)
	{
		for (i += n2 >> 1; i > 0; i >>= 1) action(i);
	}

	// 階層の降順 (範囲の降順)
	public void ForRange(int maxEx, Action<int> action)
	{
		var i = maxEx + (n2 >> 1);
		if (i == n2) { action(1); return; }
		for (; i > 1; i >>= 1) if ((i & 1) != 0) action(i - 1);
	}
}

// 範囲の和を求める場合。
class ST_Subsum : ST
{
	public ST_Subsum(int n) : base(n) { }

	public void Set(int i, long v) => Add(i, v - this[i]);
	public void Add(int i, long v) => ForLevels(i, j => a[j] += v);

	public long Subsum(int minIn, int maxEx) => Subsum(maxEx) - Subsum(minIn);
	public long Subsum(int maxEx)
	{
		var r = 0L;
		ForRange(maxEx, j => r += a[j]);
		return r;
	}
}
