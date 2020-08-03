using System;

class B2
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], k = h[1];

		var p = 0;
		for (long x = 1; x <= n; x *= k, p++) ;
		Console.WriteLine(p);
	}
}
