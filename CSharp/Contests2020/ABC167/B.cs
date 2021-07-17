using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main()
	{
		var (a, b, c, k) = Read4();
		int r = 0, t;

		k -= t = Math.Min(a, k);
		r += t;
		k -= Math.Min(b, k);
		r -= k;

		Console.WriteLine(r);
	}
}
