using System;
using System.Linq;

class E2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		int GetMedian(int[] a) => a.OrderBy(v => v).Skip((n - 1) / 2).Take(n % 2 == 0 ? 2 : 1).Sum();

		var ma = GetMedian(ps.Select(p => p[0]).ToArray());
		var mb = GetMedian(ps.Select(p => p[1]).ToArray());
		Console.WriteLine(mb - ma + 1);
	}
}
