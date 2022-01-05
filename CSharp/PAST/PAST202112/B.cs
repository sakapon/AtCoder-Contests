using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		return ps.Select(p => p.b - p.a).Sum(x =>
		{
			var r = 0;
			if (x % 10 >= 5) r++;
			if (x / 10 % 10 >= 5) r++;
			return r;
		});
	}
}
