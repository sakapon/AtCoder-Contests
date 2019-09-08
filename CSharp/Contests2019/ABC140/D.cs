using System;
using System.Linq;

class D
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var s = Console.ReadLine();
		int n = h[0], k = h[1];

		var x = Enumerable.Range(1, n - 2).Count(i => s[i] == 'L' ? s[i - 1] == 'L' : s[i + 1] == 'R');
		if (s.StartsWith("RR")) x++;
		if (s.EndsWith("LL")) x++;
		Console.WriteLine(x + Math.Max(Roll(s, n, k, true), Roll(s, n, k, false)));
	}

	static int Roll(string s, int n, int k, bool isL)
	{
		var x = 0;
		var l = s.IndexOf(isL ? 'R' : 'L'); if (l == -1) l = n;
		var r = s.LastIndexOf(isL ? 'R' : 'L');
		var c = isL ? 'L' : 'R';
		for (; k > 0; k--, x += 2, c = c == 'L' ? 'R' : 'L')
		{
			for (; l < r; l++) if (s[l] == c) break;
			for (; l < r; r--) if (s[r] == c) break;
			if (l >= r) break;
		}
		if (k > 0 && s[0] != s[n - 1]) x++;
		return x;
	}
}
