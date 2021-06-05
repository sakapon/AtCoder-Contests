using System;
using System.Collections;
using System.Linq;

class A
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[n], _ => { Console.ReadLine(); return ReadL(); });

		var amax = a.Select(ai => ai.Max()).ToArray();
		var amin = a.Select(ai => ai.Min()).ToArray();

		var rn = Enumerable.Range(0, n).ToArray();

		return Enumerable.Range(0, 1 << n)
			.Select(x => new BitArray(new[] { x }))
			.Select(ba => rn.Select(i => ba[i] ? amax[i] : amin[i]).Aggregate((x, y) => x * y))
			.Max();
	}
}
