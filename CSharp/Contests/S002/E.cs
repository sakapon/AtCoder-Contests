using System;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Split().Select(long.Parse).ToArray()).ToArray();

		Console.WriteLine(a.Sum(x => Math.Min(x[0] / 2, x[1])));
	}
}
