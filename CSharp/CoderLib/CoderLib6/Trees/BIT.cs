namespace CoderLib6.Trees
{
	// 外見上は 1-indexed, 1 <= i <= n
	// 内部では 1-indexed, 1 <= i <= n2
	// RSQ として利用します。
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

		public long this[int i] => Subsum(i) - Subsum(i - 1);

		public void Set(int i, long v) => Add(i, v - this[i]);
		public void Add(int i, long v)
		{
			for (; i <= n2; i += i & -i) a[i] += v;
		}

		public long Subsum(int minIn, int maxEx) => Subsum(maxEx - 1) - Subsum(minIn - 1);
		public long Subsum(int maxIn)
		{
			var r = 0L;
			for (var i = maxIn; i > 0; i -= i & -i) r += a[i];
			return r;
		}
	}
}
