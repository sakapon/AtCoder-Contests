using System;
using System.Linq;

class A2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var rs = new int[n].Select(_ => Array.ConvertAll(Console.ReadLine().Split(), int.Parse))
			.Select(r => new[] { Math.Min(r[0], r[2]), Math.Min(r[1], r[3]), Math.Max(r[0], r[2]), Math.Max(r[1], r[3]) })
			.ToArray();

		var ys = rs.Select(r => r[1]).Concat(rs.Select(r => r[3])).Distinct().OrderBy(v => v).ToArray();
		var yd = Enumerable.Range(0, ys.Length).ToDictionary(i => ys[i]);

		var xqs1 = rs.Where(r => r[1] == r[3]).Select(r => new[] { -1, r[0], yd[r[1]] });
		var yqs = rs.Where(r => r[0] == r[2]).Select(r => new[] { 0, r[0], yd[r[1]], yd[r[3]] });
		var xqs2 = rs.Where(r => r[1] == r[3]).Select(r => new[] { 1, r[2], yd[r[3]] });
		var qs = xqs1.Concat(yqs).Concat(xqs2).OrderBy(q => q[1]);

		var sum = 0L;
		var st = new ST1<int>(ys.Length, (x, y) => x + y, 0);
		foreach (var q in qs)
			if (q[0] == 0)
				sum += st.Get(q[2], q[3] + 1);
			else
				st.Set(q[2], st.Get(q[2]) - q[0]);
		Console.WriteLine(sum);
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
	public TV Get(int l_in, int r_ex)
	{
		int l = (n2 >> 1) + l_in, r = (n2 >> 1) + r_ex;

		var v = v0;
		while (l < r)
		{
			var length = l & -l;
			while (l + length > r) length >>= 1;
			v = Union(v, a2[l / length]);
			l += length;
		}
		return v;
	}

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
