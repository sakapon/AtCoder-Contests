using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], k = h[1];
		Console.WriteLine(Totients(k).Select((x, i) => i == 0 ? 0 : x * ((MInt)(k / i)).Pow(n)).Aggregate((x, y) => x + y));
	}

	static int[] Totients(int n)
	{
		var b = new bool[n + 1];
		for (int p = 2; p * p <= n; ++p) if (!b[p]) for (int x = p * p; x <= n; x += p) b[x] = true;
		var ps = new List<int>();
		for (int x = 2; x <= n; ++x) if (!b[x]) ps.Add(x);

		var r = new int[n + 1];
		r[1] = 1;
		for (int x = 2; x <= n; ++x)
			if (!b[x]) r[x] = x - 1;
			else foreach (var p in ps) if (x % p == 0)
					{
						var t = p;
						while (x % (t * p) == 0) t *= p;
						r[x] = x / t == 1 ? x / p * (p - 1) : r[t] * r[x / t];
						break;
					}
		return r;
	}
}

struct MInt
{
	const int M = 1000000007;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }

	public static implicit operator MInt(long v) => new MInt(v);
	public static MInt operator +(MInt x, MInt y) => x.V + y.V;
	public static MInt operator *(MInt x, MInt y) => x.V * y.V;

	public MInt Pow(int i) => MPow(V, i);
	public override string ToString() => $"{V}";

	static long MPow(long b, int i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
}
