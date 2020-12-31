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

			int r = 0, i = 0;
			string nx;
			var map = Array.ConvertAll(new bool[n / 3 + 1], _ => new HashSet<string>());
			var all = new List<string>();
			map[0].Add(t);
			all.Add(t);

			var (s11, s12) = (t.Substring(0, 1), t.Substring(1, 2));
			var (s21, s22) = (t.Substring(0, 2), t.Substring(2, 1));

			for (; i < map.Length && map[i].Count > 0; i++)
			{
				var p_all = all.ToArray();
				foreach (var x in map[i])
				{
					foreach (var p in p_all)
					{
						if (!map[i + 1].Contains(nx = p + x) && s.Contains(nx))
						{
							map[i + 1].Add(nx);
							all.Add(nx);
						}
						if (!map[i + 1].Contains(nx = x + p) && s.Contains(nx))
						{
							map[i + 1].Add(nx);
							all.Add(nx);
						}
					}

					if (!map[i + 1].Contains(nx = $"{s11}{x}{s12}") && s.Contains(nx))
					{
						map[i + 1].Add(nx);
						all.Add(nx);
					}
					if (!map[i + 1].Contains(nx = $"{s21}{x}{s22}") && s.Contains(nx))
					{
						map[i + 1].Add(nx);
						all.Add(nx);
					}
				}
			}

			for (int j = i - 1; j >= 0; j--)
			{
				foreach (var x in map[j])
				{
					while ((i = s.IndexOf(x)) != -1)
					{
						r += x.Length / 3;
						s = s.Remove(i, x.Length);
					}
				}
			}
		}
		else
		{
			int i;
			while ((i = s.IndexOf(t)) != -1)
			{
				s = s.Remove(i, 3);
			}
		}

		return (n - s.Length) / 3;
	}
}
