using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (H, M) = Read2();

		var dt = DateTime.Today + new TimeSpan(H, M, 0);

		while (true)
		{
			var h = dt.Hour.ToString("D2");
			var m = dt.Minute.ToString("D2");

			var hs = $"{h[0]}{m[0]}";
			var ms = $"{h[1]}{m[1]}";

			var h2 = int.Parse(hs);
			var m2 = int.Parse(ms);

			if (h2 < 24 && m2 < 60)
			{
				return $"{h} {m}";
			}

			dt = dt.AddMinutes(1);
		}
	}
}
