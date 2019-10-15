using System;

class B2
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		for (int p = h[0], i = 1; i <= 10; i++, p *= h[0]) Console.WriteLine(p * h[2] - h[1] * (p - 1) / (h[0] - 1));
	}
}
