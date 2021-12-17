using System;

class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		if (n == 2015) return 1;
		if (n == 2016) return 2;
		if (n <= 2017) return -1;
		return n - 2015;
	}
}
