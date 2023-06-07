using System;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var atcoder = "atcoder";
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var sl = s.ToLookup(c => c);
		var tl = t.ToLookup(c => c);

		var sc = 0;
		var tc = 0;

		for (var c = 'a'; c <= 'z'; c++)
		{
			if (atcoder.Contains(c))
			{
				var d = sl[c].Count() - tl[c].Count();
				if (d >= 0) sc += d;
				else tc += -d;
			}
			else
			{
				if (sl[c].Count() != tl[c].Count()) return false;
			}
		}
		return sc <= tl['@'].Count() && tc <= sl['@'].Count();
	}
}
