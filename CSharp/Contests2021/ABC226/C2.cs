using System;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		var u = new bool[n + 1];
		u[n] = true;
		var r = 0L;

		for (int i = n; i > 0; i--)
		{
			if (!u[i]) continue;
			r += ps[i - 1][0];

			foreach (var j in ps[i - 1].Skip(2))
			{
				u[j] = true;
			}
		}

		return r;
	}
}
