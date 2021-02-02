using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var pon = new[] { "pon", "pon", "hon", "bon", "hon", "hon", "pon", "hon", "pon", "hon", };
		var n = int.Parse(Console.ReadLine());
		Console.WriteLine(pon[n % 10]);
	}
}
