using System;
using System.Linq;

class Q047
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var tr = string.Join("", t.Select(c => c == 'G' ? 'B' : c == 'B' ? 'G' : c));
		var tg = string.Join("", t.Select(c => c == 'B' ? 'R' : c == 'R' ? 'B' : c));
		var tb = string.Join("", t.Select(c => c == 'R' ? 'G' : c == 'G' ? 'R' : c));

		var sr = new RH(s);
		var trr = new RH(tr);
		var trg = new RH(tg);
		var trb = new RH(tb);

		var c = 0;
		for (var (i, j) = (-(n - 1), n - 1); i < n; i++, j--)
		{
			var sh = sr.Hash2(Math.Max(0, i), Math.Min(n, i + n));

			var (l, r) = (Math.Max(0, j), Math.Min(n, j + n));
			if (sh == trr.Hash2(l, r) || sh == trg.Hash2(l, r) || sh == trb.Hash2(l, r)) c++;
		}
		return c;
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
}
