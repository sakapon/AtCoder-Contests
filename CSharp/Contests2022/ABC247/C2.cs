using System;

class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var s = "1";

		for (int k = 2; k <= n; k++)
		{
			s = $"{s} {k} {s}";
		}
		return s;
	}
}
