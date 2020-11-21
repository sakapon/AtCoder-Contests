using System;

class A
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = ReadL();
		long s = h[0], p = h[1];

		for (long n = 1; n <= 1000000; n++)
		{
			var m = s - n;
			if (n * m == p) { Console.WriteLine("Yes"); return; }
		}
		Console.WriteLine("No");
	}
}
