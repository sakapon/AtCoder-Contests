using System;
using System.Linq;

class E
{
	static void Main()
	{
		var k = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		// 各文字の出現回数
		var s2 = s.Select(c =>
		{
			var cs = new int[26];
			cs[c - 'a']++;
			return cs;
		}).ToArray();

		try
		{
			Console.WriteLine(Rec(k, s2, 1));
		}
		catch (Exception)
		{
			Console.WriteLine("impossible");
		}
	}

	static int Rec(int k, int[][] s, int all)
	{
		var n = s.Length;
		var n2 = n / 2;

		if (k == 0)
		{
			if (n == 0) return 0;
			if (n == 1) throw new InvalidOperationException();

			var r = 0;

			// Center
			if (n % 2 == 1)
				r += all - s[n2].Max();

			var selected = new int[n];
			for (int i = 0; i < n2; i++)
			{
				var min = 1 << 30;
				var c1 = s[i];
				var c2 = s[^(i + 1)];

				for (int j1 = 0; j1 < 26; j1++)
				{
					for (int j2 = 0; j2 < 26; j2++)
					{
						var nr = 2 * all - c1[j1] - c2[j2];
						if (min > nr)
						{
							min = nr;
							selected[i] = j1;
							selected[^(i + 1)] = j2;
						}
					}
				}
				r += min;
			}

			// All same
			if (selected.Distinct().Count() == 1)
			{
				var cj = selected[0];
				var max = s
					.Where((_, i) => n % 2 == 0 || i != n2)
					.Max(cs => cs.Where((_, j) => j != cj).Max());
				r += s[0][cj] - max;
			}

			return r;
		}
		else
		{
			if (n == 0) throw new InvalidOperationException();

			var r = 0;

			// Center
			if (n % 2 == 1)
				r += all - s[n2].Max();

			var s2 = s.Take(n2).ToArray();
			for (int i = 0; i < n2; i++)
				for (int j = 0; j < 26; j++)
					s2[i][j] += s[^(i + 1)][j];
			r += Rec(k - 1, s2, 2 * all);

			return r;
		}
	}
}
