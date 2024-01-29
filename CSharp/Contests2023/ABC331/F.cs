using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.DataTrees.SBTs;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var s = Console.ReadLine();
		var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine().Split());

		var st1 = new SBTHash(s);
		var st2 = new SBTHash(new string(s.Reverse().ToArray()));
		var b = new List<bool>();

		foreach (var q in qs)
		{
			if (q[0][0] == '1')
			{
				var x = int.Parse(q[1]) - 1;
				var c = q[2][0];
				st1[x] = c;
				st2[n - 1 - x] = c;
			}
			else
			{
				var l = int.Parse(q[1]) - 1;
				var r = int.Parse(q[2]);
				var c = (r - l) / 2;
				b.Add(st1.Hash(l, l + c) == st2.Hash(n - r, n - r + c));
			}
		}
		return string.Join("\n", b.Select(b => b ? "Yes" : "No"));
	}
}

public class SBTHash
{
	const long B = 987654323;
	const long M = 998244353;

	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);

	readonly int n;
	readonly MergeSBT<long> st;
	readonly long[] pow, pow_;

	public SBTHash(string s, long b = B)
	{
		n = s.Length;
		st = new MergeSBT<long>(n, Monoid.Int64_Add);

		pow = new long[n + 1];
		pow_ = new long[n + 1];
		pow[0] = 1;
		pow_[0] = 1;
		var binv = MInv(b);

		for (int i = 0; i < n; ++i)
		{
			pow[i + 1] = pow[i] * b % M;
			pow_[i + 1] = pow_[i] * binv % M;
			this[i] = s[i];
		}
	}

	public char this[int i]
	{
		set => st[i] = value * pow[i] % M;
	}

	public long Hash(int l, int r) => st[l, r] % M * pow_[l] % M;
}
