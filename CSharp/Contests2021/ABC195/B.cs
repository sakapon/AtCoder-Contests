using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b, w) = Read3();
		w *= 1000;

		var min = (w + b - 1) / b;
		var max = w / a;

		if (min > max) return "UNSATISFIABLE";
		return $"{min} {max}";
	}
}
