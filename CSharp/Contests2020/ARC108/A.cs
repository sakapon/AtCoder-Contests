using System;

class A
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = ReadL();
		long s = h[0], p = h[1];

		for (long n = 1; n * n <= p; n++)
			if (n * (s - n) == p) { Console.WriteLine("Yes"); return; }
		Console.WriteLine("No");
	}
}
