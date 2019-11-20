using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var x = Console.ReadLine();

		int n2 = n, b = 2;
		while (n2 % 2 == 0)
		{
			n2 /= 2;
			b *= 2;
		}
		var cycles = Enumerable.Range(1, n2).Where(i => n2 % i == 0).Select(i => b * i).ToArray();
		var counts = new MInt[cycles.Length];

		for (int i = 0; i < cycles.Length; i++)
		{
			var t = cycles[i] / 2;
			var sub = x.Substring(0, t);

			MInt count = 1, p = 1;
			for (int j = t - 1; j >= 0; j--, p *= 2)
				count += p * (sub[j] - '0');

			var subs = new[] { sub, new string(sub.Select(c => c == '0' ? '1' : '0').ToArray()) };
			var x2 = string.Join("", Enumerable.Range(0, n / t).Select(j => subs[j % 2]));
			counts[i] = count - (x.CompareTo(x2) < 0 ? 1 : 0);
		}
		for (int i = cycles.Length - 1; i > 0; i--)
			counts[i] -= counts[i - 1];

		Console.WriteLine(cycles.Zip(counts, (t, c) => t * c).Aggregate((u, v) => u + v).V);
	}
}

struct MInt
{
	const int M = 998244353;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }

	public static implicit operator MInt(long v) => new MInt(v);
	public static MInt operator +(MInt x) => x;
	public static MInt operator -(MInt x) => -x.V;
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

	public static long MFactorial(int n) { for (long x = 1, i = 1; ; x = x * ++i % M) if (i >= n) return x; }
	public static long MNpr(int n, int r)
	{
		if (n < r) return 0;
		for (long x = 1, i = n - r; ; x = x * ++i % M) if (i >= n) return x;
	}
	public static MInt MNcr(int n, int r) => n < r ? 0 : n - r < r ? MNcr(n, n - r) : (MInt)MNpr(n, r) / MFactorial(r);
}
