using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var r = Console.ReadLine();
		var c = Console.ReadLine();

		var rn = Enumerable.Range(0, n).ToArray();
		const string ABC = "ABC";

		var patterns = r.Select(CreatePatterns).ToArray();
		var g = new string[n];

		if (!DFS(0)) return "No";
		return "Yes\n" + string.Join("\n", g);

		string[] CreatePatterns(char start)
		{
			var l = new List<string>();
			var ABC_ = ABC.Replace(start.ToString(), "");
			var cs = Enumerable.Repeat('.', n).ToArray();

			for (int i = 0; i < n; i++)
			{
				cs[i] = start;
				for (int j = i + 1; j < n; j++)
				{
					for (int k = j + 1; k < n; k++)
					{
						cs[j] = ABC_[0];
						cs[k] = ABC_[1];
						l.Add(new string(cs));

						cs[j] = ABC_[1];
						cs[k] = ABC_[0];
						l.Add(new string(cs));

						cs[j] = '.';
						cs[k] = '.';
					}
				}
				cs[i] = '.';
			}
			return l.ToArray();
		}

		bool DFS(int ri)
		{
			if (ri == n) return Check();

			foreach (var s in patterns[ri])
			{
				g[ri] = s;
				if (DFS(ri + 1)) return true;
			}
			return false;
		}

		bool Check()
		{
			for (int j = 0; j < n; j++)
			{
				foreach (var c in ABC)
				{
					if (rn.Count(i => g[i][j] == c) != 1) return false;
				}

				var fi = rn.First(i => g[i][j] != '.');
				if (g[fi][j] != c[j]) return false;
			}
			return true;
		}
	}
}
