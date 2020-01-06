using System;
using System.Collections.Generic;
using System.Linq;

class G
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var rs = new int[h[1]].Select(_ => read()).ToArray();

		var map = rs.ToLookup(r => r[0], r => r[1]);
		var ins = new int[h[0] + 1];
		foreach (var r in rs) ins[r[1]]++;

		var q = new Queue<int>(Enumerable.Range(1, h[0]).Where(i => ins[i] == 0));
		var dp = new int[h[0] + 1];
		while (q.Any())
		{
			var p = q.Dequeue();
			foreach (var np in map[p])
				if (--ins[np] == 0)
				{
					dp[np] = dp[p] + 1;
					q.Enqueue(np);
				}
		}
		Console.WriteLine(dp.Max());
	}
}
