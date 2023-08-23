using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var s = Console.ReadLine();

		var dp_m = new long[8];
		var dp_e = new long[8];
		var dp_x = new long[8];

		for (int i = 0; i < n; i++)
		{
			var f = 1 << a[i];
			var c = s[i];

			if (c == 'M')
			{
				dp_m[f]++;
			}
			else if (c == 'E')
			{
				for (int j = 0; j < 8; j++)
				{
					dp_e[j | f] += dp_m[j];
				}
			}
			else
			{
				for (int j = 0; j < 8; j++)
				{
					dp_x[j | f] += dp_e[j];
				}
			}
		}

		var cs = new[] { 0, 1, 0, 2, 0, 1, 0, 3 };
		return dp_x.Zip(cs, (x, y) => x * y).Sum();
	}
}
