using System;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();
		return string.Join(" ", Enumerable.Range(1, n).OrderBy(i => p[i - 1]));
	}
}
