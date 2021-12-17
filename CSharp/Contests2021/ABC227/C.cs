using System;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var r = 0L;

		for (long a = 1; a < 10000; a++)
		{
			for (long b = a; b < 1000000; b++)
			{
				var cmax = n / (a * b);
				if (cmax < b) break;
				r += cmax - b + 1;
			}
		}

		return r;
	}
}
