using System;
using System.Collections.Generic;
using System.Linq;

class L
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var dc = t.Distinct().Count();
		if (dc == 2 && t[0] == t[2])
		{
			if (!s.Contains(t)) return 0;

			var map = Array.ConvertAll(new bool[n / 3 + 1], _ => new HashSet<string>());
			var all = new Stack<string>();
			map[1].Add(t);
			all.Push(t);

			var (s11, s12) = (t[..1], t[1..]);
			var (s21, s22) = (t[..2], t[2..]);

			void Add(int i, string nx)
			{
				if (!map[i].Contains(nx) && s.Contains(nx))
				{
					map[i].Add(nx);
					all.Push(nx);
				}
			}

			for (int i = 2; i < map.Length; i++)
			{
				foreach (var x in map[i - 1])
				{
					Add(i, $"{s11}{x}{s12}");
					Add(i, $"{s21}{x}{s22}");
				}

				for (int j = 1; j < i - j; j++)
					foreach (var x in map[j])
						foreach (var y in map[i - j])
						{
							Add(i, x + y);
							Add(i, y + x);
						}

				if (i % 2 == 0)
				{
					var a = map[i / 2].ToArray();
					for (int xi = 0; xi < a.Length; xi++)
						for (int yi = xi; yi < a.Length; yi++)
						{
							Add(i, a[xi] + a[yi]);
							Add(i, a[yi] + a[xi]);
						}
				}

				if (map[i].Count == 0) break;
			}

			foreach (var x in all)
			{
				for (int i; (i = s.IndexOf(x)) != -1;)
				{
					s = s.Remove(i, x.Length);
				}
			}
		}
		else
		{
			for (int i; (i = s.IndexOf(t)) != -1;)
			{
				s = s.Remove(i, 3);
			}
		}

		return (n - s.Length) / 3;
	}
}
