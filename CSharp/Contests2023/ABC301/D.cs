using System;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = long.Parse(Console.ReadLine());

		var r = Convert.ToInt64(s.Replace('?', '0'), 2);
		if (r > n) return -1;

		for (int i = s.Length - 1; i >= 0; i--)
		{
			if (s[^(i + 1)] != '?') continue;

			var r2 = r | (1L << i);
			if (r2 <= n) r = r2;
		}
		return r;
	}
}
