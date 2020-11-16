using System;

class B
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);
		double x1 = h[0], y1 = h[1], x2 = h[2], y2 = h[3];
		Console.WriteLine((x1 * y2 + x2 * y1) / (y1 + y2));
	}
}
