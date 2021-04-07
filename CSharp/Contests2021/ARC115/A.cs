using System;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Count(c => c == '1') % 2);
		Console.WriteLine(ps.LongCount(p => p == 0) * ps.LongCount(p => p == 1));
	}
}
