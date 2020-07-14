using System;

class A
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine(h[0] >= 10 ? h[1] : h[1] + 100 * (10 - h[0]));
	}
}
