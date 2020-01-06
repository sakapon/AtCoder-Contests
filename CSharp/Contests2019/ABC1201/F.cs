using System;
using System.Linq;

class F
{
	static void Main()
	{
		Func<long[]> read = () => Console.ReadLine().Split().Select(long.Parse).ToArray();
		var t = read();
		var a = read();
		var b = read();
		if (a[0] > b[0]) { var _ = a; a = b; b = _; }

		var sa = t.Zip(a, (u, x) => u * x).Sum();
		var sb = t.Zip(b, (u, x) => u * x).Sum();
		if (sa == sb) { Console.WriteLine("infinity"); return; }
		if (sa < sb) { Console.WriteLine(0); return; }

		var n = t[0] * (b[0] - a[0]) / (double)(sa - sb);
		Console.WriteLine((long)n * 2 + ((long)n == n ? 0 : 1));
	}
}
