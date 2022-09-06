using System;
using System.Numerics;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static Complex ReadC() { var a = Read(); return new Complex(a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var a = ReadC();
		var b = ReadC();
		var c = ReadC();
		var d = ReadC();

		if (!Check(b - a, d - a)) return false;
		if (!Check(c - b, a - b)) return false;
		if (!Check(d - c, b - c)) return false;
		if (!Check(a - d, c - d)) return false;
		return true;
	}

	static bool Check(Complex v1, Complex v2) => (v2 / v1).Phase > 0;
}
