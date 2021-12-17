using System;
using System.Collections.Generic;
using System.Linq;

class J
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var st = new STR<int>(2 * n + 1, (x, y) => x + y, 0);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var k = q[1];

			if (q[0] == 1)
			{
				var v = st.Get(k);
				Console.WriteLine(v % 2 == 0 ? k : 2 * n - k + 1);
			}
			else
			{
				st.Set(n - k + 1, n + k + 1, 1);
			}
		}
		Console.Out.Flush();
	}
}

class STR<TO>
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
	public TO[] a1;

	// (newOp, oldOp) => product
	public Func<TO, TO, TO> Multiply;
	public TO id;
	Func<TO, TO, bool> TOEquals = System.Collections.Generic.EqualityComparer<TO>.Default.Equals;

	// 全ノードを、恒等変換を表す値で初期化します。
	public STR(int n, Func<TO, TO, TO> multiply, TO _id)
	{
		while (n2 < n << 1) n2 <<= 1;
		a1 = new TO[n2];

		Multiply = multiply;
		id = _id;
		if (!TOEquals(id, default)) Init();
	}

	public void Init() { for (int i = 1; i < n2; ++i) a1[i] = id; }

	public STNode Actual(int i) => (n2 >> 1) + i;
	public int Original(STNode n) => n.i - (n2 >> 1);
	public TO this[STNode n]
	{
		get { return a1[n.i]; }
		set { a1[n.i] = value; }
	}

	void PushDown(STNode n)
	{
		var op = a1[n.i];
		if (TOEquals(op, id)) return;
		STNode c0 = n.Child0, c1 = n.Child1;
		a1[c0.i] = Multiply(op, a1[c0.i]);
		a1[c1.i] = Multiply(op, a1[c1.i]);
		a1[n.i] = id;
	}

	// Top-down
	public void Set(int l_in, int r_ex, TO op)
	{
		int al = (n2 >> 1) + l_in, ar = (n2 >> 1) + r_ex;
		Dfs(1, n2 >> 1);

		void Dfs(STNode n, int length)
		{
			int nl = n.i * length, nr = nl + length;

			if (al <= nl && nr <= ar)
			{
				// 対象のノード
				a1[n.i] = Multiply(op, a1[n.i]);
			}
			else
			{
				PushDown(n);
				var nm = nl + nr >> 1;
				if (al < nm && nl < ar) Dfs(n.Child0, length >> 1);
				if (al < nr && nm < ar) Dfs(n.Child1, length >> 1);
			}
		}
	}

	// Top-down
	public TO Get(int i)
	{
		var ai = (n2 >> 1) + i;
		var length = n2 >> 1;
		for (STNode n = 1; n.i < ai; n = ai < n.i * length + (length >> 1) ? n.Child0 : n.Child1, length >>= 1)
			PushDown(n);
		return a1[ai];
	}
}
