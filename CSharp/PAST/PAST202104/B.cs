using System;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		var i = s.IndexOf("post");
		if (i == -1) return "none";
		return i / 4 + 1;
	}
}
