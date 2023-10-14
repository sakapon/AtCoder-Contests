using System;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var z = Console.ReadLine().Split();
		var n = int.Parse(z[0]);
		var t = z[1];
		var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var m = t.Length;

		var cs0 = new long[m + 1];
		var cs1 = new long[m + 1];

		foreach (var s in ss)
		{
			cs0[CheckPrefix(s)]++;
			cs1[CheckSuffix(s)]++;
		}

		var r = 0L;
		var geqs = 0L;

		for (int j = 0; j <= m; j++)
		{
			geqs += cs1[m - j];
			r += cs0[j] * geqs;
		}
		return r;

		int CheckPrefix(string s)
		{
			var j = 0;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == t[j]) j++;
				if (j == m) break;
			}
			return j;
		}

		int CheckSuffix(string s)
		{
			var j = 0;
			for (int i = s.Length - 1; i >= 0; i--)
			{
				if (s[i] == t[m - 1 - j]) j++;
				if (j == m) break;
			}
			return j;
		}
	}
}
