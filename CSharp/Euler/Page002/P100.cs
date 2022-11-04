using System;
using System.Collections.Generic;
using System.Linq;

class P100
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		// n: total
		// m: blue
		// n * (n-1) = 2 * m * (m-1)
		// x := 2 * n - 1
		// y := 2 * m - 1

		// Pell's equation
		// x^2 - 2 y^2 = -1
		// 初期値: (x0, y0) = (1, 1)
		// 解 (x, y) は 1 + √2 の奇数乗から求められます。

		const long n_min = 1000000000000;
		var (x, y) = (1L, 1L);
		var (n, m) = (1L, 1L);

		while (n < n_min)
		{
			(x, y) = (3 * x + 4 * y, 2 * x + 3 * y);
			(n, m) = ((x + 1) / 2, (y + 1) / 2);
		}
		return m;
	}
}
