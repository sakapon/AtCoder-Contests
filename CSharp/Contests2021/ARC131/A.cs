using System;

class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = int.Parse(Console.ReadLine());
		var b = int.Parse(Console.ReadLine());
		return $"{5 * b}{a}";
	}
}
