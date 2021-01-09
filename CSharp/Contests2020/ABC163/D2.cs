using System;

class D2
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		long n = h[0], m = n - h[1] + 2;
		Console.WriteLine((-(n + 1) * m + (n + 3) * m * (m + 1) / 2 - m * (m + 1) * (2 * m + 1) / 6) % 1000000007);
	}
}
