using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		Read();
		var a = Read().ToHashSet();
		Read();
		return Read().Count(a.Contains);
	}
}
