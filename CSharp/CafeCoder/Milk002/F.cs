using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var x = ReadL();
		var y = ReadL();

		return GetSum(n, x) + GetSum(n, y);
	}

	static long GetSum(int n, long[] a)
	{
		Array.Sort(a);

		var sum = 0L;
		var l = 0L;
		for (int i = 1; i < n; i++)
		{
			l++;
			sum += l * (n - l) * (a[i] - a[i - 1]);
		}
		return sum;
	}
}
