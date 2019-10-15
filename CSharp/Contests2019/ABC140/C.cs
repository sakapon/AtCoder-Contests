using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var b = Console.ReadLine().Split().Select(int.Parse).ToArray();

		Console.WriteLine(b[0] + Enumerable.Range(0, n - 2).Sum(i => Math.Min(b[i], b[i + 1])) + b[n - 2]);
	}
}
