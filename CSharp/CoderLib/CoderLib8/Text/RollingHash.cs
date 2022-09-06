// M には素数を指定します。
namespace CoderLib8.Text
{
	public class RollingHashBuilder
	{
		const long B = 10007;
		const long M = 1000000007;
		public static long MInt(long x) => (x %= M) < 0 ? x + M : x;

		readonly long[] pow;

		public RollingHashBuilder(int n)
		{
			pow = new long[n + 1];
			pow[0] = 1;
			for (int i = 0; i < n; ++i) pow[i + 1] = pow[i] * B % M;
		}

		public RollingHash Build(string s)
		{
			var n = s.Length;
			var pre = new long[n + 1];
			for (int i = 0; i < n; ++i) pre[i + 1] = (pre[i] * B + s[i]) % M;
			return new RollingHash(pow, pre);
		}
		public RollingHash Build(long[] s)
		{
			var n = s.Length;
			var pre = new long[n + 1];
			for (int i = 0; i < n; ++i) pre[i + 1] = (pre[i] * B + s[i]) % M;
			return new RollingHash(pow, pre);
		}

		public static long Hash(string s) => Hash(s, 0, s.Length);
		public static long Hash(string s, int start, int count)
		{
			var h = 0L;
			for (int i = 0; i < count; ++i) h = (h * B + s[start + i]) % M;
			return h;
		}
		public static long Hash(long[] s) => Hash(s, 0, s.Length);
		public static long Hash(long[] s, int start, int count)
		{
			var h = 0L;
			for (int i = 0; i < count; ++i) h = (h * B + s[start + i]) % M;
			return h;
		}
	}

	public class RollingHash
	{
		readonly long[] pow, pre;
		internal RollingHash(long[] pow_, long[] pre_) { pow = pow_; pre = pre_; }
		public long Hash(int start, int count) => RollingHashBuilder.MInt(pre[start + count] - pre[start] * pow[count]);
	}
}
