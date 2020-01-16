using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => int.Parse(Console.ReadLine())).ToArray();

		var d = Enumerable.Range(1, n).Except(a).FirstOrDefault();
		Console.WriteLine(d == 0 ? "Correct" : $"{a.GroupBy(x => x).First(g => g.Count() > 1).Key} {d}");
	}
}
