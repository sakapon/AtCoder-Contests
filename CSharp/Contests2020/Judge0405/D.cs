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

		var x0 = a[0];
		var l = new List<(int value, int index)> { (x0, 1) };
		for (int i = 1; i < h[0]; i++)
		{
			var t = Gcd(x0, a[i]);
			if (t == x0) continue;
			l.Add((x0 = t, i + 1));
		}

		var r = s.Select(q =>
		{
			var x = q;
			foreach (var (v, i) in l)
			{
				x = Gcd(x, v);
				if (x == 1) return i;
			}
			return x;
		});
		Console.WriteLine(string.Join("\n", r));
	}

	static int Gcd(int x, int y) { for (int r; (r = x % y) > 0; x = y, y = r) ; return y; }
}
