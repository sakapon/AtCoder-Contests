using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(string.Join("\n", Array.ConvertAll(new bool[int.Parse(Console.ReadLine())], _ => Solve())));
	static object Solve()
	{
		var (n, m, a, b) = Read4();
		return FloorSum(n, m, a, b);
	}

	// https://github.com/atcoder/ac-library/blob/master/atcoder/internal_math.hpp
	static long FloorSum(long n, long m, long a, long b)
	{
		var r = 0L;
		while (true)
		{
			if (a >= m)
			{
				r += n * (n - 1) / 2 * (a / m);
				a %= m;
			}
			if (b >= m)
			{
				r += n * (b / m);
				b %= m;
			}

			var yMax = a * n + b;
			if (yMax < m) return r;
			(n, m, a, b) = (yMax / m, a, m, yMax % m);
		}
	}
}
