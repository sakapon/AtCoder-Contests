namespace CoderLib6.Strings
{
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

		public static long Hash(string s, long p) => Hash(s, 0, s.Length, p);
		public static long Hash(string s, int start, int count, long p)
		{
			var h = 0L;
			for (int i = 0; i < count; ++i) h = h * p + s[start + i];
			return h;
		}
	}
}
