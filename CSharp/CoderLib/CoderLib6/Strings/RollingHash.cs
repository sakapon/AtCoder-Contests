namespace CoderLib6.Strings
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/14/ALDS1_14_B
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/14/ALDS1_14_D
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_i
	class RH
	{
		string s;
		int n;
		long p;
		long[] pow, pre;

		public RH(string _s, long _p)
		{
			s = _s;
			n = s.Length;
			p = _p;

			pow = new long[n + 1];
			pow[0] = 1;
			pre = new long[n + 1];

			for (int i = 0; i < n; ++i)
			{
				pow[i + 1] = pow[i] * p;
				pre[i + 1] = pre[i] * p + s[i];
			}
		}

		public long Hash(int start, int count) => pre[start + count] - pre[start] * pow[count];
		public long Hash2(int minIn, int maxEx) => pre[maxEx] - pre[minIn] * pow[maxEx - minIn];

		public static long Hash(string s, long p) => Hash(s, 0, s.Length, p);
		public static long Hash(string s, int start, int count, long p)
		{
			var h = 0L;
			for (int i = 0; i < count; ++i) h = h * p + s[start + i];
			return h;
		}

		public static long Pow(long b, long x)
		{
			for (var r = 1L; ; b *= b)
			{
				if ((x & 1) != 0) r *= b;
				if ((x >>= 1) == 0) return r;
			}
		}
	}
}
