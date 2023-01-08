namespace CoderLib6.Strings
{
	// M には素数を指定します。
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/14/ALDS1_14_B
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/14/ALDS1_14_D
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_i
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_au
	class RH
	{
		const long B = 10007;
		const long M = 1000000007;
		static long MInt(long x) => (x %= M) < 0 ? x + M : x;

		string s;
		int n;
		long b;
		long[] pow, pre;

		public RH(string _s, long _b = B)
		{
			s = _s;
			n = s.Length;
			b = _b;

			pow = new long[n + 1];
			pow[0] = 1;
			pre = new long[n + 1];

			for (int i = 0; i < n; ++i)
			{
				pow[i + 1] = pow[i] * b % M;
				pre[i + 1] = (pre[i] * b + s[i]) % M;
			}
		}

		public long Hash(int start, int count) => MInt(pre[start + count] - pre[start] * pow[count]);
		public long Hash2(int minIn, int maxEx) => MInt(pre[maxEx] - pre[minIn] * pow[maxEx - minIn]);

		public static long Hash(string s, long b = B) => Hash(s, 0, s.Length, b);
		public static long Hash(string s, int start, int count, long b = B)
		{
			var h = 0L;
			for (int i = 0; i < count; ++i) h = (h * b + s[start + i]) % M;
			return h;
		}
	}
}
