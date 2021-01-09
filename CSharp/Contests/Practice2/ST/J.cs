using System;
using System.Collections.Generic;
using System.Linq;

class J
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];
		var a = Read();

		var st = new ST_Max(n + 2);
		for (int i = 0; i < n; i++)
			st.Set(i + 1, a[i]);
		st.Set(n + 1, 1 << 30);

		for (int k = 0; k < h[1]; k++)
		{
			var q = Read();
			if (q[0] == 1)
				st.Set(q[1], q[2]);
			else if (q[0] == 2)
				r.Add(st.Submax(q[1], q[2] + 1));
			else
				r.Add(st.FirstArg(q[1], n + 2, q[2]));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}

class ST_Max : ST
{
	public ST_Max(int n) : base(n) { }

	public void Set(int i, long v) => ForLevels(i, n => this[n] = n.k == kMax ? v : Math.Max(this[n.Child0], this[n.Child1]));

	public long Submax(int minIn, int maxEx)
	{
		var r = long.MinValue;
		ForRange(minIn, maxEx, n => r = Math.Max(r, this[n]));
		return r;
	}

	// v 以上の値を持つインデックス
	public int FirstArg(int minIn, int maxEx, long v)
	{
		var mn = new Node(kMax, 1 << 30);
		ForRange(minIn, maxEx, n =>
		{
			if (this[n] >= v && n.i << (kMax - n.k) < mn.i << (kMax - mn.k)) mn = n;
		});

		while (mn.k < kMax) mn = this[mn.Child0] >= v ? mn.Child0 : mn.Child1;
		return mn.i;
	}
}
