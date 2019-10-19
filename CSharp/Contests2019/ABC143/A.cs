using System;

class A
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine(Math.Max(0, h[0] - 2 * h[1]));
	}
}
