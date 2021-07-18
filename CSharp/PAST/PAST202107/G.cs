using System;
using System.Collections.Generic;
using System.Linq;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());

		var r = Dfs(n);

		Console.WriteLine(r.Count);
		Console.WriteLine(string.Join(" ", r));

		// x > 0
		List<long> Dfs(long x)
		{
			var p = 1L;
			while (x % 3 == 0)
			{
				x /= 3;
				p *= 3;
			}

			if (x == 1)
			{
				return new List<long> { p };
			}

			if (x % 3 == 1)
			{
				var l = Dfs(x - 1);
				l.Add(1);
				return l.Select(v => v * p).ToList();
			}
			else
			{
				var l = Dfs(x + 1);
				l.Add(-1);
				return l.Select(v => v * p).ToList();
			}
		}
	}
}
