using System;
using System.Linq;

class C
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var s = new int[h[1]].Select(_ => Console.ReadLine().Split()).ToArray();

		var qs = s.GroupBy(x => x[0], x => x[1]).Where(g => g.Contains("AC")).Select(g => g.TakeWhile(x => x == "WA").Count()).ToArray();
		Console.WriteLine($"{qs.Length} {qs.Sum()}");
	}
}
