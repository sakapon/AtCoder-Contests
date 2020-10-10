using System;
using System.Linq;

class K
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		var n = h[0];
		var a = Read();

		var st = new ST_RUSQ(n, a);

		for (int k = 0; k < h[1]; k++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Set(q[1], q[2], (q[3], q[4]));
			else
				Console.WriteLine(st.Get(q[1], q[2]));
		}
		Console.Out.Flush();
	}
}

struct Affine : IEquatable<Affine>
{
	const long M = 998244353;
	public static Affine E = (1, 0);

	public long c1, c2;
	public Affine(long _c1, long _c2) => (c1, c2) = (_c1, _c2);
	public override string ToString() => $"{c1} {c2}";
	public static implicit operator Affine((long c1, long c2) v) => new Affine(v.c1, v.c2);

	public bool Equals(Affine other) => c1 == other.c1 && c2 == other.c2;
	public static bool operator ==(Affine v1, Affine v2) => v1.Equals(v2);
	public static bool operator !=(Affine v1, Affine v2) => !v1.Equals(v2);
	public override bool Equals(object obj) => obj is Affine v && Equals(v);
	public override int GetHashCode() => (c1, c2).GetHashCode();

	public static long operator *(Affine m, long v) => (m.c1 * v + m.c2) % M;
	public static Affine operator *(Affine m1, Affine m2) => (m1.c1 * m2.c1 % M, (m1.c1 * m2.c2 + m1.c2) % M);
}

class ST_RUSQ
{
	public struct Node
	{
		public int i;
		public static implicit operator Node(int i) => new Node { i = i };

		public Node Parent => i >> 1;
		public Node Child0 => i << 1;
		public Node Child1 => (i << 1) + 1;
	}

	// Power of 2
	protected int n2 = 1;
	// original: 通常の更新
	public Affine[] a1;
	// shadow: 自身を含む子孫の集計
	public long[] a2;

	static Affine e1 = Affine.E;
	const long e2 = 0;

	const long M = 998244353;

	public ST_RUSQ(int n, int[] a)
	{
		while (n2 < n) n2 <<= 1;
		n2 <<= 1;
		a1 = new Affine[n2];
		a2 = new long[n2];

		// a1, a2 を独立に初期化。
		Array.Fill(a1, e1);
		for (int i = 0; i < n; i++)
			a2[(n2 >> 1) + i] = a[i];

		for (int i = 0; i < n; i++)
			Set(i, e1);
	}

	protected Node Actual(int i) => (n2 >> 1) + i;

	public void Set(int i, Affine v) => Set(1, n2 >> 1, Actual(i), Actual(i + 1), v);
	public void Set(int minIn, int maxEx, Affine v) => Set(1, n2 >> 1, Actual(minIn), Actual(maxEx), v);
	void Set(Node i, int length, Node l, Node r, Affine v)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return;

		if (l.i <= nl && nr <= r.i)
		{
			a1[i.i] = v * a1[i.i];
			a2[i.i] = (v * a2[i.i] + v.c2 * (length - 1)) % M;
		}
		else
		{
			if (!Equals(a1[i.i], e1))
			{
				a1[i.Child0.i] = a1[i.i] * a1[i.Child0.i];
				a1[i.Child1.i] = a1[i.i] * a1[i.Child1.i];
				a2[i.Child0.i] = (a1[i.i] * a2[i.Child0.i] + a1[i.i].c2 * ((length >> 1) - 1)) % M;
				a2[i.Child1.i] = (a1[i.i] * a2[i.Child1.i] + a1[i.i].c2 * ((length >> 1) - 1)) % M;
				a1[i.i] = e1;
			}
			Set(i.Child0, length >> 1, l, r, v);
			Set(i.Child1, length >> 1, l, r, v);
			a2[i.i] = (a2[i.Child0.i] + a2[i.Child1.i]) % M;
		}
	}

	public long Get(int i) => Get(1, n2 >> 1, Actual(i), Actual(i + 1));
	public long Get(int minIn, int maxEx) => Get(1, n2 >> 1, Actual(minIn), Actual(maxEx));
	long Get(Node i, int length, Node l, Node r)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return e2;

		if (l.i <= nl && nr <= r.i)
		{
			return a2[i.i];
		}
		else
		{
			if (!Equals(a1[i.i], e1))
			{
				a1[i.Child0.i] = a1[i.i] * a1[i.Child0.i];
				a1[i.Child1.i] = a1[i.i] * a1[i.Child1.i];
				a2[i.Child0.i] = (a1[i.i] * a2[i.Child0.i] + a1[i.i].c2 * ((length >> 1) - 1)) % M;
				a2[i.Child1.i] = (a1[i.i] * a2[i.Child1.i] + a1[i.i].c2 * ((length >> 1) - 1)) % M;
				a1[i.i] = e1;
			}
			return (Get(i.Child0, length >> 1, l, r) + Get(i.Child1, length >> 1, l, r)) % M;
		}
	}
}
