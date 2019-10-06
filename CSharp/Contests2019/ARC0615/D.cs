using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var a = read();
		var b = read();
		Console.WriteLine(Exchange(Exchange(n, a, b), b, a));
	}

	static long Exchange(long n, int[] a, int[] b)
	{
		var r = a.Zip(b, (p, q) => new { p, q }).OrderByDescending(_ => (double)_.q / _.p).ToArray();
		var M = n;
		for (long i = n / r[0].p, n1 = i * r[0].p; i >= 0; i--, n1 -= r[0].p)
			for (long j = (n - n1) / r[1].p, n2 = j * r[1].p; j >= 0; j--, n2 -= r[1].p)
			{
				var k = (n - n1 - n2) / r[2].p;
				M = Math.Max(M, i * r[0].q + j * r[1].q + k * r[2].q);
			}
		return M;
	}
}
