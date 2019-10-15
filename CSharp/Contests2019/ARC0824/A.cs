using System;
using System.Linq;

class A
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine(Enumerable.Range(1, a[1]).Select(j => new { d = j % 10, e = j / 10 }).Count(_ => _.d >= 2 && _.e >= 2 && _.d * _.e <= a[0]));
	}
}
