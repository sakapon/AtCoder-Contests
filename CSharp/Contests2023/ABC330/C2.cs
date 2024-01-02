using System;

class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var d = long.Parse(Console.ReadLine());

		long f(long x, long y) => x * x + y * y - d;

		var r = 1L << 60;

		for (int x = 0, y = 1500000; x < 1500000; x++)
		{
			while (true)
			{
				var v = f(x, y);
				if (v >= 0)
				{
					r = Math.Min(r, v);
					if (y == 0) break;
					y--;
				}
				else
				{
					r = Math.Min(r, -v);
					break;
				}
			}
		}
		return r;
	}
}
