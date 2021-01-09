using System;
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
		var r = new int[n + 1];
		for (int x = 1; x <= n; ++x) r[x] = x;
		for (int p = 2; p <= n; ++p) if (!b[p]) for (int x = p; x <= n; x += p) r[x] = r[x] / p * (p - 1);
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
	public static MInt operator -(MInt x, MInt y) => x.V - y.V;
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
