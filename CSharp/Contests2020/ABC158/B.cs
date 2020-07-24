using System;

class B
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		var q = Math.DivRem(h[0], h[1] + h[2], out var r);
		Console.WriteLine(q * h[1] + Math.Min(r, h[1]));
	}
}
