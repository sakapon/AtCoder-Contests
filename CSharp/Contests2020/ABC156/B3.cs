using System;
using System.Linq;

class B3
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], k = h[1];

		long x = 1;
		Console.WriteLine(Enumerable.Range(1, 31).First(i => (x *= k) > n));
	}
}
