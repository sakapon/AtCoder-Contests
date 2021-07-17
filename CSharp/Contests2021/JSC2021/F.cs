using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int t, int x, int y) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var (n, m, qc) = Read3();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var map = new CompressionHashMap(qs.Select(q => q.y).Append(0).ToArray());

		var a = new int[n];
		var b = new int[m];

		var a_count = new ST1<long>(map.Count, (x, y) => x + y, 0);
		var b_count = new ST1<long>(map.Count, (x, y) => x + y, 0);

		var a_sum = new ST1<long>(map.Count, (x, y) => x + y, 0);
		var b_sum = new ST1<long>(map.Count, (x, y) => x + y, 0);

		a_count.Set(map[0], n);
		b_count.Set(map[0], m);

		var r = 0L;
		foreach (var (t, x, y) in qs)
		{
			var j = x - 1;

			if (t == 1)
			{
				var y0 = a[j];
				var i = map[y];
				var i0 = map[y0];

				var sum0 = y0 * b_count.Get(map[0], i0) + b_sum.Get(i0, map.Count);
				var sum = y * b_count.Get(map[0], i) + b_sum.Get(i, map.Count);
				r += sum - sum0;

				a[j] = y;
				a_count.Set(i0, a_count.Get(i0) - 1);
				a_count.Set(i, a_count.Get(i) + 1);
				a_sum.Set(i0, a_sum.Get(i0) - y0);
				a_sum.Set(i, a_sum.Get(i) + y);
			}
			else
			{
				var y0 = b[j];
				var i = map[y];
				var i0 = map[y0];

				var sum0 = y0 * a_count.Get(map[0], i0) + a_sum.Get(i0, map.Count);
				var sum = y * a_count.Get(map[0], i) + a_sum.Get(i, map.Count);
				r += sum - sum0;

				b[j] = y;
				b_count.Set(i0, b_count.Get(i0) - 1);
				b_count.Set(i, b_count.Get(i) + 1);
				b_sum.Set(i0, b_sum.Get(i0) - y0);
				b_sum.Set(i, b_sum.Get(i) + y);
			}
			Console.WriteLine(r);
		}
		Console.Out.Flush();
	}
}

class CompressionHashMap
{
	public int[] Raw { get; }
	public int[] ReverseMap { get; }
	public Dictionary<int, int> Map { get; }
	public int this[int v] => Map[v];
	public int Count => ReverseMap.Length;

	int[] c;
	public int[] Compressed => c ??= Array.ConvertAll(Raw, v => Map[v]);

	public CompressionHashMap(int[] a)
	{
		// r = a.Distinct().OrderBy(v => v).ToArray();
		var hs = new HashSet<int>();
		foreach (var v in a) hs.Add(v);
		var r = new int[hs.Count];
		hs.CopyTo(r);
		Array.Sort(r);
		var map = new Dictionary<int, int>();
		for (int i = 0; i < r.Length; ++i) map[r[i]] = i;

		(Raw, ReverseMap, Map) = (a, r, map);
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
