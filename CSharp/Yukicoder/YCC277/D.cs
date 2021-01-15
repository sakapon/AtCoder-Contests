using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int l, int r, int i) Read3(int i) { var a = Read(); return (a[2], a[0], a[1], i); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var xs = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Enumerable.Range(0, qc).Select(Read3).ToArray();

		var mins = Array.ConvertAll(new bool[qc], _ => int.MaxValue);
		var st1 = new ST1<int>(n, Math.Max, int.MinValue);
		var st2 = new ST1<int>(n, Math.Min, int.MaxValue);

		var xqs = xs.Select((x, i) => (x, l: int.MinValue, r: 0, i)).Concat(qs).ToArray();
		// x - x_i >= 0
		var q1 = xqs.OrderBy(v => v);
		// x_i - x >= 0
		var q2 = xqs.OrderBy(v => -v.x).ThenBy(v => v.l);

		foreach (var (x, l, r, i) in q1)
			if (r == 0)
				st1.Set(i, x);
			else
			{
				var max = st1.Get(l - 1, r);
				if (max == st1.v0) continue;
				mins[i] = Math.Min(mins[i], x - max);
			}

		foreach (var (x, l, r, i) in q2)
			if (r == 0)
				st2.Set(i, x);
			else
			{
				var min = st2.Get(l - 1, r);
				if (min == st2.v0) continue;
				mins[i] = Math.Min(mins[i], min - x);
			}

		Console.WriteLine(string.Join("\n", mins));
	}
}

class ST1<TV>
{
	public struct STNode
	{
		public int i;
		public static implicit operator STNode(int i) => new STNode { i = i };
		public override string ToString() => $"{i}";

		public STNode Parent => i >> 1;
		public STNode Child0 => i << 1;
		public STNode Child1 => (i << 1) + 1;
		public STNode LastLeft(int length) => i * length;
		public STNode LastRight(int length) => (i + 1) * length;
	}

	// Power of 2
	public int n2 = 1;
	public TV[] a2;

	public Func<TV, TV, TV> Union;
	public TV v0;

	// 全ノードを、零元を表す値で初期化します (零元の Union もまた零元)。
	public ST1(int n, Func<TV, TV, TV> union, TV _v0, TV[] a0 = null)
	{
		while (n2 < n << 1) n2 <<= 1;
		a2 = new TV[n2];

		Union = union;
		v0 = _v0;
		if (!Equals(v0, default(TV)) || a0 != null) Init(a0);
	}

	public void Init(TV[] a0 = null)
	{
		if (a0 == null)
		{
			for (int i = 1; i < n2; ++i) a2[i] = v0;
		}
		else
		{
			Array.Copy(a0, 0, a2, n2 >> 1, a0.Length);
			for (int i = (n2 >> 1) + a0.Length; i < n2; ++i) a2[i] = v0;
			for (int i = (n2 >> 1) - 1; i > 0; --i) a2[i] = Union(a2[i << 1], a2[(i << 1) + 1]);
		}
	}

	public STNode Actual(int i) => (n2 >> 1) + i;
	public int Original(STNode n) => n.i - (n2 >> 1);
	public TV this[STNode n]
	{
		get { return a2[n.i]; }
		set { a2[n.i] = value; }
	}

	// Bottom-up
	public void Set(int i, TV v)
	{
		var n = Actual(i);
		a2[n.i] = v;
		while ((n = n.Parent).i > 0) a2[n.i] = Union(a2[n.Child0.i], a2[n.Child1.i]);
	}

	public TV Get(int i) => a2[(n2 >> 1) + i];
	// 範囲の昇順
	public TV Get(int l_in, int r_ex) => Aggregate(l_in, r_ex, v0, (p, n, l) => Union(p, a2[n.i]));

	// 範囲の昇順
	// (previous, node, length) => result
	public TR Aggregate<TR>(int l_in, int r_ex, TR r0, Func<TR, STNode, int, TR> func)
	{
		int al = (n2 >> 1) + l_in, ar = (n2 >> 1) + r_ex;

		var rv = r0;
		while (al < ar)
		{
			var length = al & -al;
			while (al + length > ar) length >>= 1;
			rv = func(rv, al / length, length);
			al += length;
		}
		return rv;
	}
}
