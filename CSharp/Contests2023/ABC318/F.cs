using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var x = ReadL();
		var l = ReadL();

		var rn = Enumerable.Range(0, n).ToArray();
		var set = new HashSet<long>();

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				var k = x[i] - l[j];
				if (Check(k) && !Check(k - 1)) set.Add(k);

				k = x[i] + l[j];
				if (Check(k) && !Check(k + 1)) set.Add(k + 1);
			}
		}

		var c = set.OrderBy(x => x).ToArray();
		return Enumerable.Range(0, c.Length / 2).Sum(i => c[2 * i + 1] - c[2 * i]);

		bool Check(long k)
		{
			// 非 AOT では Array.Sort<long>(), List<long>.Sort() で TLE となるため、LINQ を利用します。
			var d = x.Select(v => Math.Abs(v - k)).OrderBy(v => v).ToArray();
			return Array.TrueForAll(rn, i => d[i] <= l[i]);
		}
	}
}
