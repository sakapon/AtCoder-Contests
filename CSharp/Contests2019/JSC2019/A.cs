using System;
using System.Linq;

class A
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine(Enumerable.Range(1, a[0]).SelectMany(i => Enumerable.Range(1, a[1]).Select(j => new { i, d1 = j % 10, d2 = j / 10 })).Count(_ => _.d1 >= 2 && _.d2 >= 2 && _.i == _.d1 * _.d2));
	}
}
