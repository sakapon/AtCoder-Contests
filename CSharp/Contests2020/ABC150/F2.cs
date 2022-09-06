using System;
using System.Linq;
using System.Text;

class F2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var b = ReadL();
		var sb = new StringBuilder();

		var ad = Enumerable.Range(0, 2 * n - 1).Select(i => a[(i + 1) % n] ^ a[i % n]).ToArray();
		var bd = Enumerable.Range(0, n - 1).Select(i => b[i + 1] ^ b[i]).ToArray();

		var rh = new RollingHashBuilder(2 * n);
		var ah = rh.Build(ad);
		var bh = RollingHashBuilder.Hash(bd);

		for (int k = 0; k < n; k++)
			if (ah.Hash(k, n - 1) == bh) sb.AppendLine($"{k} {a[k] ^ b[0]}");
		Console.Write(sb);
	}
}

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
