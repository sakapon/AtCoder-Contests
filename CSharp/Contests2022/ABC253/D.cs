using System;

class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, a, b) = Read3L();
		return MultiSum(1) - MultiSum(a) - MultiSum(b) + MultiSum(Lcm(a, b));

		long MultiSum(long x)
		{
			var c = n / x;
			return x * c * (c + 1) / 2;
		}
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }
	static long Lcm(long a, long b) => a / Gcd(a, b) * b;
}
