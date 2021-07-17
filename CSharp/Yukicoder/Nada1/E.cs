using System;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		if (n == 2) return "INF";
		if (n == 3) return 6;
		if (n == 4) return 6;
		if (n == 5) return 4;
		if (n == 6) return 4;
		return 2;
	}
}
