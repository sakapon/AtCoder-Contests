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

			var selected = new int[n];
			for (int i = 0; i < n; i++)
			{
				var max = -1;

				for (int j = 0; j < 26; j++)
				{
					if (max < s[i][j])
					{
						max = s[i][j];
						selected[i] = j;
					}
				}
				r += all - max;
			}

			if (IsPalindrome(selected))
			{
				r += Enumerable.Range(0, n)
					.Where(i => n % 2 == 0 || i != n2)
					.Min(i => s[i][selected[i]] - s[i].Where((_, j) => j != selected[i]).Max());
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

	static bool IsPalindrome(int[] s)
	{
		for (int i = 0; i < s.Length; ++i) if (s[i] != s[s.Length - 1 - i]) return false;
		return true;
	}
}
