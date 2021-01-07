using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (a, b) = Read2();
		Console.WriteLine($"{Math.Max(a, b)} {a + b}");
	}
}
