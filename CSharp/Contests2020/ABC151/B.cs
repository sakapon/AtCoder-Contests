using System;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var x = h[0] * h[2] - read().Sum();
		Console.WriteLine(x <= h[1] ? Math.Max(x, 0) : -1);
	}
}
