using System;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		Console.WriteLine(a.Select(x => $"{x.Min()} {x.Max()}").Distinct().Count());
	}
}
