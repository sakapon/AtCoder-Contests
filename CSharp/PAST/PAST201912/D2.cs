using System;
using System.Linq;

class D2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => int.Parse(Console.ReadLine())).ToLookup(x => x);

		var d = Enumerable.Range(1, n).Where(x => a[x].Count() != 1).OrderBy(x => a[x].Count()).ToArray();
		Console.WriteLine(!d.Any() ? "Correct" : $"{d[1]} {d[0]}");
	}
}
