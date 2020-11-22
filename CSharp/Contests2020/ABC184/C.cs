using System;
using static System.Math;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (T, T) ToTuple2<T>(T[] a) => (a[0], a[1]);
	static void Main()
	{
		(int x, int y) p1 = ToTuple2(Read());
		(int x, int y) p2 = ToTuple2(Read());
		var (a, b) = p1;
		var (c, d) = p2;

		if (p1 == p2)
		{
			Console.WriteLine(0);
		}
		else if (a + b == c + d || a - b == c - d || Abs(a - c) + Abs(b - d) <= 3)
		{
			Console.WriteLine(1);
		}
		else if (Abs(a - b) % 2 == Abs(c - d) % 2 || Abs(a - c) + Abs(b - d) <= 6 ||
			Abs(Abs(a - b) - Abs(c - d)) <= 3 || Abs(Abs(a + b) - Abs(c + d)) <= 3)
		{
			Console.WriteLine(2);
		}
		else
		{
			Console.WriteLine(3);
		}
	}
}
