using System;
using System.Linq;

class B2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var c = Array.ConvertAll(new bool[n], _ => Read());

		var a = Enumerable.Range(0, n).Select(i => c[i][0]).ToArray();
		var b = c[0].ToArray();

		var amin = a.Min();
		var bmin = b.Min();
		if (amin + bmin < a[0]) return "No";

		a = a.Select(x => x - a[0] + bmin).ToArray();
		b = b.Select(x => x - bmin).ToArray();

		for (int i = 0; i < n; i++)
			for (int j = 0; j < n; j++)
				if (c[i][j] != a[i] + b[j]) return "No";
		return "Yes\n" + $"{string.Join(" ", a)}\n{string.Join(" ", b)}";
	}
}
