using System;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		var x = h[0] * Math.Pow(h[1], h[2] - 1);
		Console.WriteLine(x > 1000000000 ? "large" : $"{x}");
	}
}
