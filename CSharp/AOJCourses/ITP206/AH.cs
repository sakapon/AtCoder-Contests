using System;
using System.Linq;

class AH
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		Read();
		var a = Read().ToHashSet();
		var qc = Read()[0];
		var qs = Array.ConvertAll(new bool[qc], _ => Read()[0]);

		return string.Join("\n", qs.Select(k => a.Contains(k) ? 1 : 0));
	}
}
