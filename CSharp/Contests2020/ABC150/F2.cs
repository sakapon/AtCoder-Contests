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

		var ah = new IntRH(ad);
		var bh = IntRH.Hash(bd);

		for (int k = 0; k < n; k++)
			if (ah.Hash(k, n - 1) == bh) sb.AppendLine($"{k} {a[k] ^ b[0]}");
		Console.Write(sb);
	}
}

class IntRH
{
	const long B = 10007;
	const long M = 1000000007;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;

	long[] pow, pre;

	public IntRH(long[] s)
	{
		var n = s.Length;

		pow = new long[n + 1];
		pow[0] = 1;
		pre = new long[n + 1];

		for (int i = 0; i < n; ++i)
		{
			pow[i + 1] = pow[i] * B % M;
			pre[i + 1] = (pre[i] * B + s[i]) % M;
		}
	}

	public long Hash(int start, int count) => MInt(pre[start + count] - pre[start] * pow[count]);

	public static long Hash(long[] s) => Hash(s, 0, s.Length);
	public static long Hash(long[] s, int start, int count)
	{
		var h = 0L;
		for (int i = 0; i < count; ++i) h = (h * B + s[start + i]) % M;
		return h;
	}
}
