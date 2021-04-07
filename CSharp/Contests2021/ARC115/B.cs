using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var c = Array.ConvertAll(new bool[n], _ => Read());

		if (n == 1) return "Yes\n" + $"{c[0][0]}\n0";

		var b = (int[])c[0].Clone();

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				c[i][j] -= b[j];
			}
			if (c[i].Distinct().Count() > 1) return "No";
		}
		var a = Enumerable.Range(0, n).Select(i => c[i][0]).ToArray();

		var amin = a.Min();
		if (amin < 0)
		{
			if (b.Min() < -amin) return "No";

			for (int i = 0; i < n; i++)
			{
				a[i] -= amin;
				b[i] += amin;
			}
		}
		return "Yes\n" + $"{string.Join(" ", a)}\n{string.Join(" ", b)}";
	}
}
