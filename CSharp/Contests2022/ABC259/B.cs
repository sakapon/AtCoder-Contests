using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = Read();

		var r = new Complex(a[0], a[1]) * Complex.FromPolarCoordinates(1, a[2] * Math.PI / 180);
		return $"{r.Real} {r.Imaginary}";
	}
}
