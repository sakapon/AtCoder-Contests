using System;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var aM = a.Max();
		if (aM == 1) { Console.WriteLine(n); return; }

		var ps = PrimesF(2, aM);
		var cs = new int[aM + 1];
		var a2 = (int[])a.Clone();

		for (int i = 0; i < n; i++)
		{
			foreach (var p in ps)
			{
				if (p > 1000) break;
				if (a2[i] == 1) break;
				var c = 0;
				for (; a2[i] % p == 0; c++) a2[i] /= p;
				if (c > cs[p]) cs[p] = c;
			}
			if (a2[i] > 1) cs[a2[i]] = 1;
		}

		var lcm = ps.Where(p => cs[p] > 0).Select(p => ((MInt)p).Pow(cs[p])).Aggregate((x, y) => x * y);
		Console.WriteLine(a.Select(x => lcm / x).Aggregate((x, y) => x + y).V);
	}

	static int[] PrimesF(int m, int M)
	{
		var b = PrimeFlags(M);
		return Enumerable.Range(m, M - m + 1).Where(i => b[i]).ToArray();
	}
	static bool[] PrimeFlags(int M)
	{
		var rM = (int)Math.Sqrt(M);
		var b = new bool[M + 1]; b[2] = true;
		for (int i = 3; i <= M; i += 2) b[i] = true;
		for (int p = 3; p <= rM; p++) if (b[p]) for (var i = p * p; i <= M; i += 2 * p) b[i] = false;
		return b;
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
	public static MInt operator /(MInt x, MInt y) => x * y.Inv();

	public MInt Pow(int i) => MPow(V, i);
	public MInt Inv() => MPow(V, M - 2);

	static long MPow(long b, int i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
}
