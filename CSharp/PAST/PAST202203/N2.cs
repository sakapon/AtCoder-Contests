using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Numerics;

class N2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var c1 = new long[200001];
		foreach (var x in a) c1[x]++;
		var c2 = (long[])c1.Clone();
		Array.Reverse(c2);

		var r = FFT.Convolution(c1, c2);
		return r.Count(x => x > 0);
	}
}
