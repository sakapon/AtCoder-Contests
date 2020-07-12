using System;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		long a = h[0], r = h[1], n = h[2];

		for (int i = 1; i < n; i++)
			if ((a *= r) > 1000000000) { Console.WriteLine("large"); return; }
		Console.WriteLine(a);
	}
}
