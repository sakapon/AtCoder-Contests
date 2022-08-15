using System;

class A
{
	static void Main() => Console.WriteLine(Solve() ? "YES" : "NO");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var k = int.Parse(Console.ReadLine());
		return n / 2 >= k;
	}
}
