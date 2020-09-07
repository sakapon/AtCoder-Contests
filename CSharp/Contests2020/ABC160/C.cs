using System;
using System.Linq;

class C
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToList();
		int k = h[0], n = h[1];
		var a = Console.ReadLine().Split().Select(int.Parse).ToList();
		a.Add(k + a[0]);
		Console.WriteLine(k - Enumerable.Range(0, n).Max(i => a[i + 1] - a[i]));
	}
}
