using System;
using System.Linq;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s0 = "atcoder";
		var s = Console.ReadLine().ToList();

		var r = 0;
		for (int i = 0; i < s0.Length; i++)
		{
			var c = s0[i];
			var j = s.IndexOf(c);
			r += j - i;
			s.RemoveAt(j);
			s.Insert(i, c);
		}
		return r;
	}
}
