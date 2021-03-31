using System;
using System.Linq;

class A2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int a) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, l) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2()).Prepend((x: 0, a: 0)).ToArray();

		var r = 0L;
		var t = 0L;
		var (min, max) = (1L << 60, 0L);
		for (int i = 1; i <= n; i++)
		{
			if (max < (t += ps[i].x - ps[i - 1].x))
			{
				max = t;
				min = 1L << 60;
			}
			min = Math.Min(min, t -= ps[i].a);
			r = Math.Max(r, max - min);
		}
		Console.WriteLine(r);
	}
}
