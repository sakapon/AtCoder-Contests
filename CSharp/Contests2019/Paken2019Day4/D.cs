using System;
using System.Linq;

class D
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], x = h[1];

		var k = Enumerable.Range(1, n).TakeWhile(i => i * (i + 1) / 2 <= x).LastOrDefault();
		var q = Enumerable.Repeat(1, k);
		if (k < n) q = q.Concat(new[] { k + 1 - (x - k * (k + 1) / 2) }).Concat(Enumerable.Repeat(k + 1, n - k - 1));

		Console.WriteLine(k);
		Console.WriteLine(string.Join(" ", q));
	}
}
