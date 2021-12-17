using System;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		return "/MABCDEFP".IndexOf(s[0]);
	}
}
