using System;

class A
{
	static void Main()
	{
		var h = int.Parse(Console.ReadLine());
		var w = int.Parse(Console.ReadLine());
		var n = int.Parse(Console.ReadLine());

		Console.WriteLine(Math.Ceiling((double)n / Math.Max(h, w)));
	}
}
