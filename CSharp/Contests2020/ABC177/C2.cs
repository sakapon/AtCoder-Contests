using System;

class C2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var r = 0L;
		var s = 0L;

		foreach (var x in a)
		{
			r += x * s;
			r %= M;
			s += x;
			s %= M;
		}
		return r;
	}

	const long M = 1000000007;
}
