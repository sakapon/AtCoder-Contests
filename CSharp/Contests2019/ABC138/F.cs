using System;
using System.Linq;

class F
{
	static void Main()
	{
		var M = 1000000007;
		var a = Console.ReadLine().Split().Select(long.Parse).ToArray();
		long L = a[0], R = a[1];

		var c = 0L;
		for (var x = L; x <= R; x++)
		{
			var p = Math.Floor(Math.Log(x, 2));
			var y_max = Math.Min((long)Math.Pow(2, p + 1) - 1, R);
			for (var y = x; y <= y_max; y++)
				if (y - x == (y ^ x)) c++;
			c %= M;
		}
		Console.WriteLine(c);
	}
}
