using System;

class A
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		long n = h[0], a = h[1], b = h[2], d = b - a;
		Console.WriteLine(d % 2 == 0 ? d / 2 : Math.Min(a, n - b + 1) + (d - 1) / 2);
	}
}
