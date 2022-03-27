using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		Console.ReadLine();
		var s = Console.ReadLine();

		var set = new HashSet<string>();
		var l = new List<string>();
		l.Add("UTPC");

		for (int k = 0; k < 3; k++)
		{
			var ps = l.ToArray();
			l.Clear();

			foreach (var p in ps)
			{
				if (set.Contains(p)) continue;
				set.Add(p);
				if (Regex.IsMatch(s, p.Replace("*", @"\S"))) return k;

				var cs = p.ToCharArray();
				for (int i = 0; i < 4; i++)
				{
					if (cs[i] == '*') continue;
					var c = cs[i];
					cs[i] = '*';
					l.Add(new string(cs));
					cs[i] = c;
				}

				for (int i = 0; i < 4; i++)
				{
					for (int j = i + 1; j < 4; j++)
					{
						(cs[i], cs[j]) = (cs[j], cs[i]);
						l.Add(new string(cs));
						(cs[i], cs[j]) = (cs[j], cs[i]);
					}
				}
			}
		}
		return 3;
	}
}
