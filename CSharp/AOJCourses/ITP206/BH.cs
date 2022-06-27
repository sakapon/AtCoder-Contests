using System;
using System.Linq;

class BH
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		Read();
		var a = Read().ToHashSet();
		Read();
		return Array.TrueForAll(Read(), a.Contains) ? 1 : 0;
	}
}
