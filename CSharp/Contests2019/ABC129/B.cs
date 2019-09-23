using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var w = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine(Enumerable.Range(1, n - 1).Min(i => Math.Abs(w.Take(i).Sum() - w.Skip(i).Sum())));
	}
}
