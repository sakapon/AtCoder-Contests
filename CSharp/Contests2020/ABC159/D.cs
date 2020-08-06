using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var d = a.GroupBy(x => x).ToDictionary(g => g.Key, g => g.LongCount());
		var M = d.Sum(p => p.Value * (p.Value - 1) / 2);
		Console.WriteLine(string.Join("\n", a.Select(x => M - d[x] + 1)));
	}
}
