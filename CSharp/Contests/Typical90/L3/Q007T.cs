using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees;

class Q007T
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => int.Parse(Console.ReadLine()));

		var set = new AvlSet<int>();
		foreach (var v in a) set.Add(v);
		set.Add(-1 << 30);
		set.Add(int.MaxValue);

		return string.Join("\n", qs.Select(GetMin));

		int GetMin(int bv)
		{
			var av2 = set.GetMin(x => x >= bv);
			var av1 = set.GetPrevious(av2);
			return Math.Min(av2 - bv, bv - av1);
		}
	}
}
