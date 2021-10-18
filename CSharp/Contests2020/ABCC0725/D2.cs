using System;

class D2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var r = 1000L;

		for (int i = 0; i < n - 1; i++)
		{
			if (a[i] < a[i + 1])
			{
				var q = r / a[i];
				r += q * (a[i + 1] - a[i]);
			}
		}
		return r;
	}
}
