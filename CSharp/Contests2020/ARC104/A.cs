using System;

class A
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine($"{(h[0] + h[1]) / 2} {(h[0] - h[1]) / 2}");
	}
}
