using System;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var k = int.Parse(Console.ReadLine());

		var r = 0L;

		for (int i = 1; i < 1000000; i++)
		{
			r *= 10;
			r += 7;
			r %= k;

			if (r == 0) return i;
		}
		return -1;
	}
}
