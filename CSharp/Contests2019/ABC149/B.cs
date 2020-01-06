using System;

class B
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		Console.WriteLine($"{Math.Max(0, h[0] - h[2])} {Math.Max(0, h[1] - Math.Max(0, h[2] - h[0]))}");
	}
}
