using System;

class B
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], k = h[1];
		Console.WriteLine(MNpr(k, n));
	}

	const long M = 1000000007;
	static long MNpr(int n, int r)
	{
		if (n < r) return 0;
		for (long x = 1, i = n - r; ; x = x * ++i % M) if (i >= n) return x;
	}
}
