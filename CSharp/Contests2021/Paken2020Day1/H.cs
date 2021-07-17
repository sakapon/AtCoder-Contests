using System;
using System.Linq;

class H
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve() ? "Yes" : "No")));
	static bool Solve()
	{
		var (a, b, c) = Read3L();

		bool? isOdd = null;

		for (int i = 0; i < 60; i++)
		{
			var f = 1 << i;
			var af = (a & f) != 0;
			var bf = (b & f) != 0;
			var cf = (c & f) != 0;

			if (af)
			{
				if (bf)
				{
					if (isOdd == null || isOdd == cf)
					{
						isOdd = cf;
					}
					else
					{
						return false;
					}
				}
				else
				{

				}
			}
			else
			{
				if (bf)
				{
					return false;
				}
				else
				{
					if (cf) return false;
				}
			}
		}
		return true;
	}
}
