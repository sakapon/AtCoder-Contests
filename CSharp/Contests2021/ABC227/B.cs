using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Read();

		var u = new bool[1001];

		for (int a = 1; a <= 1000; a++)
		{
			for (int b = 1; b <= 1000; b++)
			{
				var t = 4 * a * b + 3 * a + 3 * b;
				if (t > 1000) break;

				u[t] = true;
			}
		}

		return s.Count(x => !u[x]);
	}
}
