using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, x) = Read2();
		var a = Read();
		Console.WriteLine(string.Join(" ", a.Where(v => v != x)));
	}
}
