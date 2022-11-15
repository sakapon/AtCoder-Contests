using System;
using System.Collections.Generic;
using System.Linq;
using static EulerLib8.Common;

class P062
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const int count = 5;

		var dc = new Dictionary<string, int>();
		var dv = new Dictionary<string, long>();

		var r = (s: "", c: 0);

		for (long n = 1; r.c < count; n++)
		{
			var c = n * n * n;
			var cs = c.ToString().ToCharArray();
			Array.Sort(cs);
			var s = new string(cs);
			if (dc.ContainsKey(s))
			{
				dc[s]++;
			}
			else
			{
				dc[s] = 1;
				dv[s] = c;
			}
			ChMax(ref r, (s, dc[s]), p => p.Item2);
		}
		return dv[r.s];
	}
}
