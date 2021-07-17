using System;
using System.Linq;
using System.Numerics;

class B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		if (a.Any(x => x == 0)) { Console.WriteLine(0); return; }

		BigInteger r = 1, M = 1000000000000000000;
		foreach (var x in a)
			if ((r *= x) > M) { Console.WriteLine(-1); return; }
		Console.WriteLine(r);
	}
}
