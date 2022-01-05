using System;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();

		var r = Enumerable.Range(1, n).Select(i => (-a[i - 1] - b[i - 1], -a[i - 1], i)).ToArray();
		Array.Sort(r);
		return string.Join(" ", r.Select(t => t.i));
	}
}
