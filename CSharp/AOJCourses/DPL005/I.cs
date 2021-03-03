using System;

class I
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], k = h[1];

		MInt r = 0, c = 1, f = 1;
		for (int i = 0; i < k; i++)
		{
			r += c * MInt.MPow(k - i, n);
			c *= -(k - i);
			c /= i + 1;
			f *= i + 1;
		}
		Console.WriteLine(r / f);
	}
}
