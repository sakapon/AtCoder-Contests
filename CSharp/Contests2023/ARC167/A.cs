using System;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = ReadL();

		Array.Sort(a);
		var b = a[(n - m)..];

		for (int i = 0; i < n - m; i++)
		{
			b[n - m - 1 - i] += a[i];
		}
		return b.Sum(x => x * x);
	}
}
