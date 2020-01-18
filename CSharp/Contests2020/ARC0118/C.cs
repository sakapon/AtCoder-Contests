using System;
using System.Linq;

class C
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], k = h[1], s = h[2], M = 1000000000;

		var a = Enumerable.Repeat(s, k).Concat(Enumerable.Repeat(s == M ? 1 : M, n - k));
		Console.WriteLine(string.Join(" ", a));
	}
}
