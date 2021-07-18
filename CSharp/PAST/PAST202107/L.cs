using System;
using System.Collections.Generic;
using System.Linq;

class L
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int x, int y, int r) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var vs = a.ToList();
		foreach (var (t, x, y) in qs)
		{
			if (t == 1)
			{
				vs.Add(y);
			}
		}
		var vMap = new CompressionHashMap(vs.ToArray());

		// キーの値となりうるインデックスのリスト
		var map0 = Array.ConvertAll(new bool[vMap.Count], _ => new List<int>());

		for (int i = 0; i < n; i++)
		{
			map0[vMap[a[i]]].Add(i);
		}

		foreach (var (t, x, y) in qs)
		{
			if (t == 1)
			{
				map0[vMap[y]].Add(x - 1);
			}
		}

		var map = Array.ConvertAll(map0, l =>
		{
			var b = l.ToArray();
			Array.Sort(b);
			return b;
		});
		var u = Array.ConvertAll(map, p => new bool[p.Length]);
		var mapInv = Array.ConvertAll(map, p => ToInverseMap(p));

		for (int i = 0; i < n; i++)
		{
			u[vMap[a[i]]][mapInv[vMap[a[i]]][i]] = true;
		}

		var st = new ST1<int>(n, Math.Min, int.MaxValue, a);

		var r = new List<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var (t, x, y) = q;
			x--;

			if (t == 1)
			{
				u[vMap[a[x]]][mapInv[vMap[a[x]]][x]] = false;
				a[x] = y;
				st.Set(x, y);
				u[vMap[a[x]]][mapInv[vMap[a[x]]][x]] = true;
			}
			else
			{
				r.Clear();

				var p = st.Get(x, y);
				var inds = map[vMap[p]];
				var j0 = First(0, inds.Length, j => inds[j] >= x);

				for (int j = j0; j < inds.Length && inds[j] < y; j++)
				{
					if (u[vMap[p]][j])
					{
						r.Add(inds[j] + 1);
					}
				}
				Console.WriteLine($"{r.Count} " + string.Join(" ", r));
			}
		}
		Console.Out.Flush();
	}

	static int[] ToInverseMap(int[] a, int max)
	{
		var d = Array.ConvertAll(new bool[max + 1], _ => -1);
		for (int i = 0; i < a.Length; ++i) d[a[i]] = i;
		return d;
	}

	static Dictionary<T, int> ToInverseMap<T>(T[] a)
	{
		var d = new Dictionary<T, int>();
		for (int i = 0; i < a.Length; ++i) d[a[i]] = i;
		return d;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}

class MultiMap<TK, TV> : Dictionary<TK, List<TV>>
{
	static List<TV> empty = new List<TV>();

	public void Add(TK key, TV v)
	{
		if (ContainsKey(key)) this[key].Add(v);
		else this[key] = new List<TV> { v };
	}

	public List<TV> ReadValues(TK key) => ContainsKey(key) ? this[key] : empty;
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
