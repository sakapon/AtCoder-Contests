using System;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		long a = h[0], b = h[1], c = h[2], d = c - a - b;
		Console.WriteLine(d > 0 && 4 * a * b < d * d ? "Yes" : "No");
	}
}
