using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();

		var max_a = 0L;
		var max = 0L;
		var c = new long[n];

		for (int i = 0; i < n; i++)
		{
			ChFirstMax(ref max_a, a[i]);
			ChFirstMax(ref max, max_a * b[i]);
			c[i] = max;
		}

		return string.Join("\n", c);
	}

	public static void ChFirstMax<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) < 0) o1 = o2; }
}
