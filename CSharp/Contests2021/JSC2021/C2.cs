using System;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (a, b) = Read2();
		Console.WriteLine(Enumerable.Range(1, b - a).Reverse().First(d => (a - 1) / d + 1 < b / d));
	}
}
