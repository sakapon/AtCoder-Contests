using System;

namespace CoderLib6.Trees.SegmentTrees
{
	class ST_RASQ
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
		public long[] a1;
		// shadow: 自身を含む子孫の集計
		public long[] a2;

		const long e1 = 0;
		const long e2 = 0;

		public ST_RASQ(int n)
		{
			while (n2 < n) n2 <<= 1;
			n2 <<= 1;
			a1 = new long[n2];
			a2 = new long[n2];
		}

		protected Node Actual(int i) => (n2 >> 1) + i;

		public void Set(int i, long v) => Set(1, n2 >> 1, Actual(i), Actual(i + 1), v);
		public void Set(int minIn, int maxEx, long v) => Set(1, n2 >> 1, Actual(minIn), Actual(maxEx), v);
		void Set(Node i, int length, Node l, Node r, long v)
		{
			int nl = i.i * length, nr = nl + length;
			if (r.i <= nl || nr <= l.i) return;

			if (l.i <= nl && nr <= r.i)
			{
				a1[i.i] += v;
				a2[i.i] += v * length;
			}
			else
			{
				if (a1[i.i] != e1)
				{
					a1[i.Child0.i] += a1[i.i];
					a1[i.Child1.i] += a1[i.i];
					a2[i.Child0.i] += a1[i.i] * (length >> 1);
					a2[i.Child1.i] += a1[i.i] * (length >> 1);
					a1[i.i] = e1;
				}
				Set(i.Child0, length >> 1, l, r, v);
				Set(i.Child1, length >> 1, l, r, v);
				a2[i.i] = a2[i.Child0.i] + a2[i.Child1.i];
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
				if (a1[i.i] != e1)
				{
					a1[i.Child0.i] += a1[i.i];
					a1[i.Child1.i] += a1[i.i];
					a2[i.Child0.i] += a1[i.i] * (length >> 1);
					a2[i.Child1.i] += a1[i.i] * (length >> 1);
					a1[i.i] = e1;
				}
				return Get(i.Child0, length >> 1, l, r) + Get(i.Child1, length >> 1, l, r);
			}
		}
	}
}
