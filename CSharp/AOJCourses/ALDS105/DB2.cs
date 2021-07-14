using System;
using System.Linq;

class DB2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).Select((x, i) => new { x, i }).OrderBy(_ => _.x).Select(_ => _.i + 1).ToArray();

		Console.WriteLine(InversionNumber(n, a));
	}

	static long InversionNumber(int n, int[] a)
	{
		var r = 0L;
		var bit = new BIT(n);
		for (int i = 0; i < n; ++i)
		{
			r += i - bit.Sum(a[i]);
			bit.Add(a[i], 1);
		}
		return r;
	}
}
