using System;
using System.Linq;

class D
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], x = h[1];

		var k = Enumerable.Range(1, n).TakeWhile(i => i * (i + 1) / 2 <= x).LastOrDefault();
		var a = Enumerable.Repeat(1, k).Concat(Enumerable.Repeat(k + 1, n - k)).ToArray();
		if (k < n) a[k] = k + 1 - (x - k * (k + 1) / 2);

		Console.WriteLine(k);
		Console.WriteLine(string.Join(" ", a));
	}
}
