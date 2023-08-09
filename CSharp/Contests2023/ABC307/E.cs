using System;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();

		var (r0, r1) = (0L, 1L);

		for (int i = 0; i < n - 2; i++)
		{
			(r0, r1) = (r1, (r0 * (m - 1) + r1 * (m - 2)) % M);
		}
		return r1 * m % M * (m - 1) % M;
	}

	const long M = 998244353;
}
