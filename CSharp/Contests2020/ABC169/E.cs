using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		if (n % 2 == 0)
		{
			var ma = ps.Select(v => v.a).OrderBy(v => v).Skip(n / 2 - 1).Take(2).Sum();
			var mb = ps.Select(v => v.b).OrderBy(v => v).Skip(n / 2 - 1).Take(2).Sum();
			Console.WriteLine(mb - ma + 1);
		}
		else
		{
			var ma = ps.Select(v => v.a).OrderBy(v => v).ElementAt(n / 2);
			var mb = ps.Select(v => v.b).OrderBy(v => v).ElementAt(n / 2);
			Console.WriteLine(mb - ma + 1);
		}
	}
}
