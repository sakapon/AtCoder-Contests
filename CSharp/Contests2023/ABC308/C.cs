using System;
using System.Linq;

class C
{
	static decimal[] ReadDec() => Array.ConvertAll(Console.ReadLine().Split(), decimal.Parse);
	static (decimal, decimal) Read2Dec() { var a = ReadDec(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2Dec());

		var r = Enumerable.Range(1, n).OrderBy(i =>
		{
			var (a, b) = ps[i - 1];
			return -a / (a + b);
		});
		return string.Join(" ", r);
	}
}
