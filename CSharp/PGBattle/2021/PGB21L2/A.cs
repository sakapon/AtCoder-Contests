using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var p = int.Parse(Console.ReadLine()) / 100D;

		var ps = Enumerable.Range(0, 8).Select(Mul).ToArray();

		var r = 0D;
		for (uint x = 0; x < 1 << 7; x++)
		{
			var pc = BitOperations.PopCount(x);
			if (pc >= 4)
			{
				r += ps[pc];
			}
		}
		r *= 100;
		return $"{r:F9}";

		double Mul(int n)
		{
			var r = 1D;
			for (int i = 0; i < n; i++)
				r *= p;
			for (int i = 0; i < 7 - n; i++)
				r *= 1 - p;
			return r;
		}
	}
}
