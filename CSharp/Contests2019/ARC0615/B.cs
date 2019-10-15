using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();
		if (n == 1) { Console.WriteLine(1); return; }

		var l = new List<string>();
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
			{
				var p = ps[i][0] - ps[j][0];
				var q = ps[i][1] - ps[j][1];
				if (p == 0) q = Math.Abs(q);
				else if (p < 0) { p = -p; q = -q; }
				l.Add($"{p} {q}");
			}
		Console.WriteLine(n - l.GroupBy(x => x).Max(g => g.Count()));
	}
}
