using System;
using System.Linq;

class H
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var c = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var s = new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var ne = n / 2;
		var mo = c.Where((x, i) => i % 2 == 0).Min();
		var me = ne == 0 ? 0 : c.Where((x, i) => i % 2 == 1).Min();
		var m = new[] { mo, me };
		var set = new[] { 0L, 0 };
		var r = 0L;
		foreach (var q in s)
		{
			if (q[0] == 1)
			{
				var i = q[1] - 1;
				if (c[i] - set[i % 2] < q[2]) continue;
				c[i] -= q[2];
				m[i % 2] = Math.Min(m[i % 2], c[i]);
				r += q[2];
			}
			else if (q[0] == 2)
			{
				if (m[0] - set[0] < q[1]) continue;
				set[0] += q[1];
			}
			else
			{
				if (m[0] - set[0] < q[1]) continue;
				if (ne > 0 && m[1] - set[1] < q[1]) continue;
				set[0] += q[1];
				set[1] += q[1];
			}
		}
		Console.WriteLine(r + (n - ne) * set[0] + ne * set[1]);
	}
}
