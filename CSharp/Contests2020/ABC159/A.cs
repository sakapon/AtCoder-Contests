using System;

class A
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], m = h[1];
		Console.WriteLine((n * (n - 1) + m * (m - 1)) / 2);
	}
}
