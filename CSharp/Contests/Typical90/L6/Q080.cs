using System;
using System.Numerics;

class Q080
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static ulong[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), ulong.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, d) = Read2();
		var a = ReadL();

		var r = 0L;
		for (uint x = 0; x < 1 << n; x++)
		{
			ulong or = 0;
			for (int i = 0; i < n; i++)
			{
				if ((x & (1 << i)) != 0)
				{
					or |= a[i];
				}
			}
			var orpop = BitOperations.PopCount(or);

			var xpop = BitOperations.PopCount(x);
			if (xpop % 2 == 0)
			{
				r += 1L << d - orpop;
			}
			else
			{
				r -= 1L << d - orpop;
			}
		}
		return r;
	}
}
