using System;
using System.Linq;
using System.Numerics;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static decimal[] ReadDec() => Array.ConvertAll(Console.ReadLine().Split(), decimal.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		if (a.Any(x => x == 0)) { Console.WriteLine(0); return; }

		BigInteger M = 1000000000000000000;
		BigInteger r = 1;
		foreach (var x in a)
			if ((r *= x) > M) { Console.WriteLine(-1); return; }
		Console.WriteLine(r);
	}
}
