namespace CoderLib6.DataTrees
{
	// 外見上は 1-indexed, 1 <= i <= n
	// 内部では 1-indexed, 1 <= i <= n2
	// RSQ として利用します。
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/5/ALDS1_5_D
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/2/DSL_2_B
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_b
	class BIT
	{
		// Power of 2
		int n2 = 1;
		long[] a;

		public BIT(int n)
		{
			while (n2 < n) n2 <<= 1;
			a = new long[n2 + 1];
		}

		public long this[int i]
		{
			get { return Sum(i) - Sum(i - 1); }
			set { Add(i, value - this[i]); }
		}

		public void Add(int i, long v)
		{
			for (; i <= n2; i += i & -i) a[i] += v;
		}

		public long Sum(int l_in, int r_ex) => Sum(r_ex - 1) - Sum(l_in - 1);
		public long Sum(int r_in)
		{
			var r = 0L;
			for (var i = r_in; i > 0; i -= i & -i) r += a[i];
			return r;
		}
	}
}
