using System;

class F
{
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

		var r = 0L;
		for (int i = 1; i < n; i++)
		{
			r += (a[i] - a[i - 1]) * i * (n - i);
		}
		return r;
	}
}
