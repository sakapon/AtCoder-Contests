using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var c = Read();

		var cmin = c.Min();
		var length = n / cmin;
		var vmin = Enumerable.Range(1, 9).Last(v => c[v - 1] == cmin);

		var r = new int[length];
		Array.Fill(r, vmin);
		n %= cmin;

		for (int i = 0; i < length; i++)
		{
			for (int v = 9; v > vmin; v--)
			{
				var d = c[v - 1] - cmin;
				if (d <= n)
				{
					r[i] = v;
					n -= d;
					break;
				}
			}
		}
		return string.Join("", r);
	}
}
