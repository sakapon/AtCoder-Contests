using System;
using System.Linq;

class Q004L
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());

		var rh = Enumerable.Range(0, h).ToArray();
		var rw = Enumerable.Range(0, w).ToArray();

		var si = a.Select(r => r.Sum()).ToArray();
		var sj = rw.Select(j => rh.Sum(i => a[i][j])).ToArray();

		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				a[i][j] = si[i] + sj[j] - a[i][j];

		foreach (var r in a)
			Console.WriteLine(string.Join(" ", r));
	}
}
