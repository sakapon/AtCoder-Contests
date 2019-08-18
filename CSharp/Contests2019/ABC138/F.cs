using System;
using System.Linq;

class F
{
	static void Main()
	{
		var M = 1000000007;
		var a = Console.ReadLine().Split().Select(long.Parse).ToArray();

		var c = 0L;
		for (var x = a[0]; x <= a[1]; x++)
			for (var y = x; y <= a[1]; y++)
				if ((y % x) == (y ^ x)) c++;
		Console.WriteLine(c);
	}
}
