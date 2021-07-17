using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());
		var a = Read();

		var d = a.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
		return a.Sum(x => n - d[x]) / 2;
	}
}
