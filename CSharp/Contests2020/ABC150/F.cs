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

		var a2h = Array.ConvertAll(a2s, s => new RH(s));
		var b0h = Array.ConvertAll(b0s, s => RH.Hash(s));
		var b1h = Array.ConvertAll(b1s, s => RH.Hash(s));

		for (int k = 0; k < n; k++)
		{
			var x = a[k] ^ b[0];
			var ok = Array.TrueForAll(r30, i => a2h[i].Hash(k, n) == ((x & (1 << i)) == 0 ? b0h : b1h)[i]);
			if (ok) sb.AppendLine($"{k} {x}");
		}
		Console.Write(sb);
	}
}

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
