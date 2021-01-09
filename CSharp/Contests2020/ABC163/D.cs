using System;

class D
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		long n = h[0], k = h[1];

		long r = 1, M = n * (n + 1) / 2, m = M;
		for (long i = n; i >= k; i--)
			r += (M -= n - i) - (m -= i) + 1;
		Console.WriteLine(r % 1000000007);
	}
}
