using System;
using System.Linq;

class C
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int k = h[0], n = h[1];
		var a = Read();
		a = a.Append(k + a[0]).ToArray();
		Console.WriteLine(k - Enumerable.Range(0, n).Max(i => a[i + 1] - a[i]));
	}
}
