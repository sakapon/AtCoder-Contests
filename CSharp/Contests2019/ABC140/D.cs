using System;
using System.Linq;

class D
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var s = Console.ReadLine();
		Console.WriteLine(h[0] - 1 + Math.Min(0, 2 * h[1] - Enumerable.Range(0, h[0] - 1).Count(i => s[i] != s[i + 1])));
	}
}
