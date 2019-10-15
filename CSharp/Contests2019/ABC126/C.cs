using System;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);
		double x = h[0], k = h[1], s = 0, p = 1, c;

		while (x > 0)
		{
			x -= c = Math.Max(Math.Floor(x - k / p + 1), 0);
			s += c / p;
			p *= 2;
		}
		Console.WriteLine(s / h[0]);
	}
}
