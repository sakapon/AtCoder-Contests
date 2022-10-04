using System;
using System.Linq;
using System.Text;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = Read()[0];
		var a = Read();
		var b = Read();
		var sb = new StringBuilder();

		var r30 = Enumerable.Range(0, 30).ToArray();
		var rn = Enumerable.Range(0, n).ToArray();

		var a2 = a.Concat(a).ToArray();
		var a2s = r30.Select(i => new string(a2.Select(v => (v & (1 << i)) != 0 ? '1' : '0').ToArray())).ToArray();
		var b0s = r30.Select(i => new string(b.Select(v => (v & (1 << i)) != 0 ? '1' : '0').ToArray())).ToArray();
		var b1s = r30.Select(i => new string(b.Select(v => (v & (1 << i)) == 0 ? '1' : '0').ToArray())).ToArray();

		var rh = new RollingHashBuilder(2 * n);
		var a2h = Array.ConvertAll(a2s, s => rh.Build(s));
		var b0h = Array.ConvertAll(b0s, s => RollingHashBuilder.Hash(s));
		var b1h = Array.ConvertAll(b1s, s => RollingHashBuilder.Hash(s));

		for (int k = 0; k < n; k++)
		{
			var x = a[k] ^ b[0];
			var ok = Array.TrueForAll(r30, i => a2h[i].Hash(k, n) == ((x & (1 << i)) == 0 ? b0h : b1h)[i]);
			if (ok) sb.AppendLine($"{k} {x}");
		}
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
