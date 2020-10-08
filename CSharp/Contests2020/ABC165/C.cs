using System;
using System.Linq;

class C
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var qs = new int[h[2]].Select(_ => Read()).ToArray();

		var r = 0;
		var rn = Enumerable.Range(0, n + m - 1).ToArray();

		for (int x = 0; x < 1 << n + m - 1; x++)
		{
			var a = rn.Where(i => (x & (1 << i)) != 0).Select((i, j) => i - j).ToArray();
			if (a.Length != n) continue;
			r = Math.Max(r, qs.Where(q => a[q[1] - 1] - a[q[0] - 1] == q[2]).Sum(q => q[3]));
		}
		Console.WriteLine(r);
	}
}
