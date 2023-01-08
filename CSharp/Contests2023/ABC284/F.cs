using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Strings;

class F
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var t = Console.ReadLine();
		var tr = new string(t.Reverse().ToArray());

		var rh = new RH(t);
		var rhr = new RH(tr);

		for (int i = 0; i <= n; i++)
		{
			if (rh.Hash(0, i) == rhr.Hash(n - i, i) && rh.Hash(n + i, n - i) == rhr.Hash(n, n - i))
			{
				var s = t[..i] + t[(n + i)..];
				return $"{s}\n{i}";
			}
		}
		return -1;
	}
}
