using System;
using System.Collections.Generic;
using System.Linq;

class M
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var d = a.GroupBy(x => x).ToDictionary(g => g.Key, g => g.LongCount());
		var sum = d.Values.Sum(c => c * (c - 1) / 2);

		void Add(int x, int count)
		{
			var c = d.GetValueOrDefault(x);
			sum -= c * (c - 1) / 2;
			c += count;
			sum += c * (c - 1) / 2;
			d[x] = c;
		}

		var ruq = new STR<int>(n, (x, y) => x == -1 ? y : x, -1);
		var lengths = new int[n];
		lengths[^1] = 1;

		for (int i = n - 2; i >= 0; i--)
		{
			if (a[i] == a[i + 1])
			{
				lengths[i] = lengths[i + 1] + 1;
			}
			else
			{
				lengths[i] = 1;
				ruq.Set(i + 1, i + 1 + lengths[i + 1], i + 1);
			}
		}
		ruq.Set(0, lengths[0], 0);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var (l, r, x) = q;
			l--;
			var length = r - l;

			var L = ruq.Get(l);
			if (ruq.Get(r - 1) == L)
			{
				// 同一区間内
				var X = a[L];
				var R = L + lengths[L];

				Add(X, -length);
				Add(x, length);

				lengths[L] = l - L;

				a[l] = x;
				lengths[l] = length;
				ruq.Set(l, r, l);

				if (r < R)
				{
					a[r] = X;
					lengths[r] = R - r;
					ruq.Set(r, R, r);
				}
			}
			else
			{
				var LR = L + lengths[L];

				var RL = ruq.Get(r - 1);
				var R = RL + lengths[RL];

				// 関連する区間を全て除きます。
				for (int i = L; i < R; i += lengths[i])
				{
					Add(a[i], -lengths[i]);
				}

				Add(a[L], l - L);
				Add(a[RL], R - r);
				Add(x, length);

				lengths[L] = l - L;

				a[l] = x;
				lengths[l] = length;
				ruq.Set(l, r, l);

				if (r < R)
				{
					a[r] = a[RL];
					lengths[r] = R - r;
					ruq.Set(r, R, r);
				}
			}

			Console.WriteLine(sum);
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
