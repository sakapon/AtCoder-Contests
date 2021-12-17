using System;

class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		return $"AGC{(n < 42 ? n : n + 1):D3}";
	}
}
