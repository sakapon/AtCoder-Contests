using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees
{
	// n: 個数
	// k: 階層のインデックス
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/5/ALDS1_5_D
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_b
	class ST
	{
		public struct Node
		{
			public int k, i;
			public Node(int _k, int _i) { k = _k; i = _i; }

			public Node Parent => new Node(k - 1, i >> 1);
			public Node Child0 => new Node(k + 1, i << 1);
			public Node Child1 => new Node(k + 1, (i << 1) + 1);
		}

		protected int kMax;
		List<long[]> vs = new List<long[]> { new long[1] };

		public ST(int n)
		{
			for (int c = 1; c < n; vs.Add(new long[c <<= 1])) ;
			kMax = vs.Count - 1;
		}

		public virtual long this[int i] => vs[kMax][i];
		public long this[Node n]
		{
			get { return vs[n.k][n.i]; }
			set { vs[n.k][n.i] = value; }
		}

		public void InitAllLevels(long v)
		{
			foreach (var a in vs) for (int i = 0; i < a.Length; ++i) a[i] = v;
		}

		public void Clear()
		{
			for (int k = 0; k <= kMax; ++k) Array.Clear(vs[k], 0, vs[k].Length);
		}

		public void ForLevels(int i, Action<Node> action)
		{
			for (int k = kMax; k >= 0; --k, i >>= 1) action(new Node(k, i));
		}

		// インデックスの昇順ではなく、階層の降順です。
		public void ForRange(int minIn, int maxEx, Action<Node> action)
		{
			for (int k = kMax, f = 1; k >= 0 && minIn < maxEx; --k, f <<= 1)
			{
				if ((minIn & f) != 0) action(new Node(k, (minIn += f) / f - 1));
				if ((maxEx & f) != 0) action(new Node(k, (maxEx -= f) / f));
			}
		}
	}

	// 範囲の和を求める場合。
	class ST_Subsum : ST
	{
		public ST_Subsum(int n) : base(n) { }

		public void Set(int i, long v) => Add(i, v - this[i]);
		public void Add(int i, long v) => ForLevels(i, n => this[n] += v);

		public long Subsum(int minIn, int maxEx)
		{
			var r = 0L;
			ForRange(minIn, maxEx, n => r += this[n]);
			return r;
		}
	}

	// 範囲に加算する場合。
	class ST_RangeAdd : ST
	{
		public ST_RangeAdd(int n) : base(n) { }

		public void Add(int minIn, int maxEx, long v) => ForRange(minIn, maxEx, n => this[n] += v);

		public override long this[int i]
		{
			get
			{
				var r = 0L;
				ForLevels(i, n => r += this[n]);
				return r;
			}
		}
	}

	// 範囲の最小値を求める場合。
	class ST_Min : ST
	{
		public ST_Min(int n) : base(n) { }

		public void Set(int i, long v) => ForLevels(i, n => this[n] = n.k == kMax ? v : Math.Min(this[n.Child0], this[n.Child1]));

		public long Submin(int minIn, int maxEx)
		{
			var r = long.MaxValue;
			ForRange(minIn, maxEx, n => r = Math.Min(r, this[n]));
			return r;
		}

		public int FirstArgMin(int minIn, int maxEx)
		{
			var m = long.MaxValue;
			var mn = new Node();
			ForRange(minIn, maxEx, n =>
			{
				if (this[n] < m)
				{
					m = this[n];
					mn = n;
				}
			});

			while (mn.k < kMax) mn = this[mn.Child0] == m ? mn.Child0 : mn.Child1;
			return mn.i;
		}
	}

	// 範囲の最大値を求める場合。
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
	}

	// 範囲の bit OR を求める場合。
	class ST_BitOR : ST
	{
		public ST_BitOR(int n) : base(n) { }

		public void Set(int i, long v) => ForLevels(i, n => this[n] = n.k == kMax ? v : this[n.Child0] | this[n.Child1]);

		public long Subor(int minIn, int maxEx)
		{
			var r = 0L;
			ForRange(minIn, maxEx, n => r |= this[n]);
			return r;
		}
	}
}
