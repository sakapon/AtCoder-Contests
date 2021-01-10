using System;

class J
{
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		Solve();
		Console.Out.Flush();
	}

	static void Solve()
	{
		int count = 0;
		for (int p = 1; ; p++)
			for (int q = 1; q < p; q++)
			{
				var (a, b, c) = (2 * p * q, p * p - q * q, p * p + q * q);
				if (Gcd(a, b) != 1) continue;
				if (a > b) (a, b) = (b, a);
				Console.WriteLine($"{a} {b} {c}");
				if (++count == 100000) return;
			}
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
