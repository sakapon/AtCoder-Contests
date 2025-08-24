using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.DataTrees.SBTs;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var s = Console.ReadLine();
		var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine().Split());

		var st1 = new SBTRollingHash(s);
		var st2 = new SBTRollingHash(new string(s.Reverse().ToArray()));
		var b = new List<bool>();

		foreach (var q in qs)
		{
			if (q[0][0] == '1')
			{
				var x = int.Parse(q[1]) - 1;
				var c = q[2][0];
				st1[x] = c;
				st2[n - 1 - x] = c;
			}
			else
			{
				var l = int.Parse(q[1]) - 1;
				var r = int.Parse(q[2]);
				var c = (r - l) / 2;
				b.Add(st1.Hash(l, l + c) == st2.Hash(n - r, n - r + c));
			}
		}
		return string.Join("\n", b.Select(b => b ? "Yes" : "No"));
	}
}
