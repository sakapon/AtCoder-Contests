using System;
using System.Linq;

class B
{
	static long[] Read() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var n2 = n / 2;
		var a = Read();
		var b = Read();

		var d0 = Enumerable.Range(0, n2).Select(i => 2 * i).Select(i => a[i] - b[i]).OrderBy(x => -x).ToArray();
		var d1 = Enumerable.Range(0, n2).Select(i => 2 * i + 1).Select(i => a[i] - b[i]).OrderBy(x => -x).ToArray();

		long M = 0, t = 0;
		for (int i = 0; i < n2; i++)
			M = Math.Max(M, t += d0[i] + d1[i]);
		Console.WriteLine(b.Sum() + M);
	}
}
