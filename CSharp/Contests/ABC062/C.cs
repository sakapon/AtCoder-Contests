using System;
using System.Linq;
using static System.Math;

class C
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(long.Parse).ToArray();
		long h = a[0], w = a[1];

		if (h % 3 == 0 || w % 3 == 0) { Console.WriteLine(0); return; }
		var m = Min(h > 3 ? w : long.MaxValue, w > 3 ? h : long.MaxValue);
		m = Min(m, Divide(h, w));
		m = Min(m, Divide(w, h));
		Console.WriteLine(m);
	}

	static long Divide(long h, long w)
	{
		var h2 = h / 2;
		var m = long.MaxValue;
		for (long v = (w + 1) / 2, v2 = w - v; v > 0; v--, v2++)
		{
			var ss = new[] { h * v, h2 * v2, (h - h2) * v2 };
			Array.Sort(ss);
			m = Min(m, ss[2] - ss[0]);
		}
		return m;
	}
}
