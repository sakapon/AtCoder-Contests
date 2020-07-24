using System;

class A
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine(Math.Min(Math.Max(h[0], h[1]), h[2]));
	}
}
