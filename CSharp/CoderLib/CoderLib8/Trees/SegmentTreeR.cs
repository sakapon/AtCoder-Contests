using System;

namespace CoderLib8.Trees
{
	// 範囲更新・一点取得
	// ST1 の双対となる概念です。
	// T は作用素を表します。
	class STR<T>
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
		public T[] a;

		// (newOp, oldOp) => product
		public Func<T, T, T> Multiply;
		public T id;
		Func<T, T, bool> TEquals = System.Collections.Generic.EqualityComparer<T>.Default.Equals;

		// 全ノードを、恒等変換を表す値で初期化します。
		public STR(int n, Func<T, T, T> multiply, T _id)
		{
			while (n2 < n) n2 <<= 1;
			a = new T[n2 <<= 1];

			Multiply = multiply;
			id = _id;
			if (!TEquals(id, default(T))) Init();
		}

		public STNode Actual(int i) => (n2 >> 1) + i;
		public int Original(STNode n) => n.i - (n2 >> 1);
		public T this[STNode n]
		{
			get { return a[n.i]; }
			set { a[n.i] = value; }
		}

		public void Init() { for (int i = 1; i < n2; ++i) a[i] = id; }

		void PushDown(STNode n)
		{
			if (TEquals(a[n.i], id)) return;
			STNode c0 = n.Child0, c1 = n.Child1;
			a[c0.i] = Multiply(a[n.i], a[c0.i]);
			a[c1.i] = Multiply(a[n.i], a[c1.i]);
			a[n.i] = id;
		}

		// Top-down
		public void Set(int l_in, int r_ex, T v)
		{
			int al = (n2 >> 1) + l_in, ar = (n2 >> 1) + r_ex;
			Dfs(1, n2 >> 1);

			void Dfs(STNode n, int length)
			{
				var nl = n.i * length;
				var nr = nl + length;

				if (al <= nl && nr <= ar)
				{
					// 対象のノード
					a[n.i] = Multiply(v, a[n.i]);
				}
				else
				{
					PushDown(n);
					var nm = (nl + nr) >> 1;
					if (al < nm && nl < ar) Dfs(n.Child0, length >> 1);
					if (al < nr && nm < ar) Dfs(n.Child1, length >> 1);
				}
			}
		}

		// Top-down
		public T Get(int i)
		{
			var ai = (n2 >> 1) + i;
			var length = n2 >> 1;
			for (STNode n = 1; n.i < ai; n = ai < n.i * length + (length >> 1) ? n.Child0 : n.Child1, length >>= 1)
				PushDown(n);
			return a[ai];
		}
	}
}
