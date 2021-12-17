using System;

class A2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b) = Read2();
		return Divide(a, b, 2, 1);
	}

	static double Divide(double v1, double v2, double m, double n) => (n * v1 + m * v2) / (m + n);
}
