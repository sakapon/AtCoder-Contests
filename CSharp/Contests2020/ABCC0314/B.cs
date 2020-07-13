using System;

class B
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		Console.WriteLine(h[0] == 1 || h[1] == 1 ? 1 : (h[0] * h[1] + 1) / 2);
	}
}
