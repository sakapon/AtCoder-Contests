using System;

class A
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine() + Console.ReadLine();
		return s != "#..#" && s != ".##.";
	}
}
