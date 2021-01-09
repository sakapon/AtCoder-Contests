using System;
using System.Linq;

class C2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var m = new int[n];
		var j = 0;
		p.Aggregate(1 << 30, (a, x) => m[j++] = Math.Min(a, x));
		Console.WriteLine(Enumerable.Range(0, n).Count(i => p[i] <= m[i]));
	}
}
