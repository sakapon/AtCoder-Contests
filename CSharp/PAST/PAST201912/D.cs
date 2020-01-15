using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => int.Parse(Console.ReadLine())).ToArray();

		var gs = a.GroupBy(x => x).Where(g => g.Count() > 1).ToArray();
		Console.WriteLine(gs.Length == 0 ? "Correct" : $"{gs[0].Key} {Enumerable.Range(1, n).Except(a).First()}");
	}
}
