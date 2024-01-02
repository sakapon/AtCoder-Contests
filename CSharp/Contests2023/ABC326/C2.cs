using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections.Statics.Typed;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();

		Array.Sort(a);
		var set = new ArrayItemSet<int>(a);
		return a.Max(x => set.GetCount(x, x + m));
	}
}
