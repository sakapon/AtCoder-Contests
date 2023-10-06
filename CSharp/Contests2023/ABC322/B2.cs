using System;

class B2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		Console.ReadLine();
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var r = 0;
		if (!t.StartsWith(s)) r += 2;
		if (!t.EndsWith(s)) r += 1;
		return r;
	}
}
