using System;

class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var r = n * 108 / 100;
		return r < 206 ? "Yay!" : r == 206 ? "so-so" : ":(";
	}
}
