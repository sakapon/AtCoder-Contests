using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var a = Read();
		var b = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		const int inv = 0;
		static int Gcd2(int x, int y) => x == inv ? y : y == inv ? x : Gcd(x, y);

		var sta = new ST1<int>(n, Gcd2, inv, Enumerable.Range(0, n - 1).Select(i => Math.Abs(a[i + 1] - a[i])).ToArray());
		var stb = new ST1<int>(n, Gcd2, inv, Enumerable.Range(0, n - 1).Select(i => Math.Abs(b[i + 1] - b[i])).ToArray());

		return string.Join("\n", qs.Select(q =>
		{
			var g = a[q[0] - 1] + b[q[2] - 1];
			g = Gcd2(g, sta.Get(q[0] - 1, q[1] - 1));
			g = Gcd2(g, stb.Get(q[2] - 1, q[3] - 1));
			return g;
		}));
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
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
