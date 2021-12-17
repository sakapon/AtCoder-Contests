using System;
using System.Linq;
using System.Numerics;

class I2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static Complex ReadComplex() { var a = Read(); return new Complex(a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p1 = ReadComplex();
		var p2 = ReadComplex();
		var abs = Array.ConvertAll(new bool[n], _ => ReadComplex());

		var d = (p1 + p2) / 2;
		var p12 = p2 - p1;
		var rot = Complex.Conjugate(p12 / p12.Magnitude);
		var r = abs.Select(p => (p - d) * rot)
			.Select(p => $"{p.Real} {p.Imaginary}");

		return string.Join("\n", r);
	}
}
