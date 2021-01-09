using System;

class A
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine(60 * (h[2] - h[0]) + h[3] - h[1] - h[4]);
	}
}
