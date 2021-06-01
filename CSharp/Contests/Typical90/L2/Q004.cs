using System;

class Q004
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());

		var si = new int[h];
		var sj = new int[w];

		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
			{
				si[i] += a[i][j];
				sj[j] += a[i][j];
			}

		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				a[i][j] = si[i] + sj[j] - a[i][j];

		foreach (var r in a)
			Console.WriteLine(string.Join(" ", r));
	}
}
