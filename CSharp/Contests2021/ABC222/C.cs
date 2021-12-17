using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[2 * n], _ => Console.ReadLine());

		var people = Enumerable.Range(0, 2 * n).Select(i => new Person { id = i }).ToArray();

		for (int j = 0; j < m; j++)
		{
			for (int i = 0; i < n; i++)
			{
				var c0 = ps[people[2 * i].id][j];
				var c1 = ps[people[2 * i + 1].id][j];

				var r = Game(c0, c1);
				if (r == -1)
				{
					people[2 * i].wins++;
				}
				else if (r == 1)
				{
					people[2 * i + 1].wins++;
				}
			}

			people = people.OrderBy(p => -p.wins).ThenBy(p => p.id).ToArray();
		}

		return string.Join("\n", people.Select(p => p.id + 1));
	}

	static int Game(char c, char d)
	{
		if (c == d) return 0;
		if (c == 'G' && d == 'P') return 1;
		if (c == 'C' && d == 'G') return 1;
		if (c == 'P' && d == 'C') return 1;
		return -1;
	}

	class Person
	{
		public int id, wins;
	}
}
