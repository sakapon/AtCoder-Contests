using System;
using System.Linq;
using CoderLib8.Values;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		(int a, int b)[] ps = Array.ConvertAll(new bool[n], _ => Read2());

		var rs = Array.ConvertAll(ps, p => new Rational(p.a, p.a + p.b));
		return string.Join(" ", Enumerable.Range(1, n).OrderBy(i => -rs[i - 1]));
	}
}
