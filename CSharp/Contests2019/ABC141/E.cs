using System;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var d = s.Select((c, i) => new { c, i }).GroupBy(_ => _.c).Where(g => g.Count() > 1).ToDictionary(g => g.Key, g => g.Select(_ => _.i).ToArray());
		int l = 0, r = n / 2, m = r / 2;
		for (; l < r;)
		{
			var ok = false;
			foreach (var p in d)
			{
				for (int i = 0; i < p.Value.Length; i++)
				{
					for (int j = i + 1; j < p.Value.Length; j++)
					{
						if (p.Value[j] - p.Value[i] < m || n - p.Value[j] < m) continue;
						if (Match(s, p.Value[i], p.Value[j], m))
						{
							ok = true;
							break;
						}
					}
					if (ok) break;
				}
				if (ok) break;
			}
			if (ok)
			{
				l = m;
				m = (l + r) / 2;
				if (m == l) m = r;
			}
			else
			{
				r = m - 1;
				m = (l + r) / 2;
			}
		}
		Console.WriteLine(m);
	}

	static bool Match(string s, int s1, int s2, int c)
	{
		for (int i = 0; i < c; i++) if (s[s1 + i] != s[s2 + i]) return false;
		return true;
	}
}
