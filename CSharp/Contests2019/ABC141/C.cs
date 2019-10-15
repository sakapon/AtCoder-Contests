using System;
using System.Linq;

class C
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var d = new int[h[2]].Select(_ => int.Parse(Console.ReadLine())).GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
		for (var i = 1; i <= h[0]; i++) Console.WriteLine(h[1] - h[2] + (d.ContainsKey(i) ? d[i] : 0) > 0 ? "Yes" : "No");
	}
}
