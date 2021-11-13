using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();

		var mc = new MCombination(n);

		var r = 0L;
		Partition(n, p =>
		{
			var lcm = 1;
			var comb = 1L;
			var rem = n;

			var g = p.GroupBy(v => v).Select(g => mc.MInvFactorial(g.Count())).Aggregate((x, y) => x * y % M);

			foreach (var v in p)
			{
				lcm = Lcm(lcm, v);
				comb = comb * mc.MNcr(rem, v) % M * mc.MFactorial(v - 1) % M;
				rem -= v;
			}

			r += MPow(lcm, k) * comb % M * g % M;
		});

		return r % M;
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
	static int Lcm(int a, int b) => a / Gcd(a, b) * b;

	public static void Partition(int n, Action<int[]> action)
	{
		Dfs(new[] { n });

		void Dfs(int[] p)
		{
			action(p);

			var v1 = p[^1];
			for (int i = p.Length == 1 ? 1 : p[^2]; i << 1 <= v1; ++i)
			{
				var q = new int[p.Length + 1];
				Array.Copy(p, 0, q, 0, p.Length - 1);
				q[^2] = i;
				q[^1] = v1 - i;
				Dfs(q);
			}
		}
	}
}

public class MCombination
{
	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);

	static long[] MFactorials(int n)
	{
		var f = new long[n + 1];
		f[0] = 1;
		for (int i = 1; i <= n; ++i) f[i] = f[i - 1] * i % M;
		return f;
	}

	// nPr, nCr を O(1) で求めるため、階乗を O(n) で求めておきます。
	long[] f, f_;
	public MCombination(int nMax)
	{
		f = MFactorials(nMax);
		f_ = Array.ConvertAll(f, MInv);
	}

	public long MFactorial(int n) => f[n];
	public long MInvFactorial(int n) => f_[n];
	public long MNpr(int n, int r) => n < r ? 0 : f[n] * f_[n - r] % M;
	public long MNcr(int n, int r) => n < r ? 0 : f[n] * f_[n - r] % M * f_[r] % M;

	// nMax >= 2n としておく必要があります。
	public long MCatalan(int n) => f[2 * n] * f_[n] % M * f_[n + 1] % M;
}
