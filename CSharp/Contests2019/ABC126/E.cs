using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var n = h[0];
		var map = new int[n + 1].Select(_ => new List<int>()).ToArray();
		foreach (var r in new int[h[1]].Select(_ => read()))
		{
			map[r[0]].Add(r[1]);
			map[r[1]].Add(r[0]);
		}

		var c = 0;
		var used = new bool[n + 1];
		var q = new Queue<int>();
		for (int i = 1; i <= n; i++)
		{
			if (used[i]) continue;
			c++;
			q.Enqueue(i);

			while (q.Any())
			{
				var p = q.Dequeue();
				used[p] = true;
				foreach (var np in map[p])
				{
					if (used[np]) continue;
					q.Enqueue(np);
				}
			}
		}
		Console.WriteLine(c);
	}
}
