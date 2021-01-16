using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, k) = Read2();
		var a = Read();

		var m = a.Sum();
		long r = 0, p = 1;

		for (int i = 0; i < n; i++)
		{
			r += a[n - 1 - i] * p;
			p = p * k % m;
		}
		Console.WriteLine(r % m);
	}
}
