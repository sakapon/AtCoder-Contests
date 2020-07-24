using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var d = new int[n].Select(_ => Console.ReadLine()).GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

		var m = d.Max(p => p.Value);
		Console.WriteLine(string.Join("\n", d.Where(p => p.Value == m).Select(p => p.Key).OrderBy(x => x)));
	}
}
