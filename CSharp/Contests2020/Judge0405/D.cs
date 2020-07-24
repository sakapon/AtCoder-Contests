using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var a = Read();
		var s = Read();

		var y = a[0];
		var l = new List<(int, int)> { (y, 1) };
		for (int t, i = 1; i < h[0]; i++)
			if ((t = Gcd(y, a[i])) != y)
				l.Add((y = t, i + 1));

		int Solve(int x)
		{
			foreach (var (v, i) in l)
				if ((x = Gcd(x, v)) == 1) return i;
			return x;
		}
		Console.WriteLine(string.Join("\n", s.Select(Solve)));
	}

	static int Gcd(int x, int y) { for (int r; (r = x % y) > 0; x = y, y = r) ; return y; }
}
