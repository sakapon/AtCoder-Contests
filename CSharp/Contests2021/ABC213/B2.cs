using System;
using System.Linq;

class B2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		// ソートしない方法
		var amax1 = a.Max();
		var amax2 = a.Max(x => x == amax1 ? 0 : x);
		return Array.IndexOf(a, amax2) + 1;
	}
}
