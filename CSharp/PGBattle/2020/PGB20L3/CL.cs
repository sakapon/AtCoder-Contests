using System;
using System.Collections.Generic;
using System.Linq;

class CL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, d) = Read3L();
		var a = ReadL();

		Array.Sort(a);
		var un = a.GroupBy(x => x % d).Select(g => Unavailable(g.Key, g.ToArray())).Aggregate((x, y) => x + y);
		var all = GetAll();
		return all - un;

		MInt GetAll()
		{
			MInt all = 0;
			if (d <= 1 << 20)
			{
				// 余りごとに加算
				for (long rem = 0; rem < d; rem++)
				{
					// 範囲内の個数
					var k = (n + d - rem) / d;
					if (rem == 0) k--;
					all += Sum1(k);
				}
			}
			else
			{
				// 選んだ個数ごとに加算
				var max = n / d + 1;
				for (long c = 1; c <= max; c++)
				{
					// 占有する幅
					var w = (c - 1) * d + 1;
					if (n < w) continue;
					all += n - w + 1;
				}
			}
			return all;
		}

		MInt Unavailable(long rem, long[] a)
		{
			// 範囲内の個数
			var k = (n + d - rem) / d;
			if (rem == 0) k--;
			var all = Sum1(k);

			for (int i = 0; i < a.Length; i++)
			{
				a[i] = (a[i] - 1) / d;
			}

			a = a.Prepend(-1).Append(k).ToArray();
			for (int i = 1; i < a.Length; i++)
			{
				var x = a[i] - a[i - 1] - 1;
				all -= Sum1(x);
			}
			return all;
		}

		static MInt Sum1(MInt k)
		{
			return k * (k + 1) / 2;
		}
	}
}

struct MInt
{
	const long M = 1000000007;
	//const long M = 998244353;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }
	public override string ToString() => $"{V}";
	public static implicit operator MInt(long v) => new MInt(v);

	public static MInt operator -(MInt x) => -x.V;
	public static MInt operator +(MInt x, MInt y) => x.V + y.V;
	public static MInt operator -(MInt x, MInt y) => x.V - y.V;
	public static MInt operator *(MInt x, MInt y) => x.V * y.V;
	public static MInt operator /(MInt x, MInt y) => x.V * y.Inv().V;

	public static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	public MInt Pow(long i) => MPow(V, i);
	public MInt Inv() => MPow(V, M - 2);
}
