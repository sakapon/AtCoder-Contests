using System;
using System.Collections.Generic;
using System.Linq;

class Q031
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var w = Read();
		var b = Read();

		// (w, b)
		var dpwb = new MemoDP2<int>(51, 2000, -1, (dp, i, j) =>
		{
			var t = new bool[2000];

			for (int k = j / 2; k > 0; k--)
			{
				t[dp[i, j - k]] = true;
			}
			if (i > 0)
			{
				t[dp[i - 1, j + i]] = true;
			}

			for (int k = 0; k < t.Length; k++)
			{
				if (!t[k]) return k;
			}
			throw new InvalidOperationException();
		});
		dpwb[0, 1] = 0;

		var xor = w.Zip(b, (i, j) => dpwb[i, j]).Aggregate((x, y) => x ^ y);
		return xor != 0 ? "First" : "Second";
	}
}
