using System;
using System.Linq;

class B3
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], k = h[1];
		Console.WriteLine(Enumerable.Range(1, 30).First(i => (n /= k) == 0));
	}
}
