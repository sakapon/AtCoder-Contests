using System;
using System.Linq;

class D2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var u = new bool[n + 1];
		var c = new long[n + 1];

		for (int x = 500; x > 0; x--)
		{
			var x2 = x * x;

			for (int i = x2; i <= n; i += x2)
			{
				if (u[i]) continue;
				u[i] = true;
				c[i / x2]++;
			}
		}

		return c.Sum(x => x * x);
	}
}
