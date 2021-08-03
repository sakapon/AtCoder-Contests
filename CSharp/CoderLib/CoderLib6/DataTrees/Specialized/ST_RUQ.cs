using System;

namespace CoderLib6.DataTrees.Specialized
{
	class ST_RUQ
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
		public long[] a;

		const long e = long.MaxValue;

		public ST_RUQ(int n)
		{
			while (n2 < n) n2 <<= 1;
			a = new long[n2 <<= 1];
		}

		public void InitAllLevels(long v) { for (int i = 1; i < a.Length; ++i) a[i] = v; }

		protected Node Actual(int i) => (n2 >> 1) + i;

		public void Set(int i, long v) => Set(1, n2 >> 1, Actual(i), Actual(i + 1), v);
		public void Set(int minIn, int maxEx, long v) => Set(1, n2 >> 1, Actual(minIn), Actual(maxEx), v);
		void Set(Node i, int length, Node l, Node r, long v)
		{
			int nl = i.i * length, nr = nl + length;
			if (r.i <= nl || nr <= l.i) return;

			if (l.i <= nl && nr <= r.i)
			{
				a[i.i] = v;
			}
			else
			{
				if (a[i.i] != e)
				{
					a[i.Child0.i] = a[i.i];
					a[i.Child1.i] = a[i.i];
					a[i.i] = e;
				}
				Set(i.Child0, length >> 1, l, r, v);
				Set(i.Child1, length >> 1, l, r, v);
			}
		}

		public long Get(int i) => Get(1, n2 >> 1, Actual(i), Actual(i + 1));
		public long Get(int minIn, int maxEx) => Get(1, n2 >> 1, Actual(minIn), Actual(maxEx));
		long Get(Node i, int length, Node l, Node r)
		{
			int nl = i.i * length, nr = nl + length;
			if (r.i <= nl || nr <= l.i) return e;

			if (l.i <= nl && nr <= r.i)
			{
				return a[i.i];
			}
			else
			{
				if (a[i.i] != e)
				{
					a[i.Child0.i] = a[i.i];
					a[i.Child1.i] = a[i.i];
					a[i.i] = e;
				}
				return Math.Min(Get(i.Child0, length >> 1, l, r), Get(i.Child1, length >> 1, l, r));
			}
		}
	}
}
