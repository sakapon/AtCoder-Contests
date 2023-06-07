using System;

class C
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var atcoder = "atcoder";
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var ds = new int[1 << 7];
		var sc = 0;
		var tc = 0;

		foreach (var c in s)
			if (c == '@') sc++;
			else ds[c]++;
		foreach (var c in t)
			if (c == '@') tc++;
			else ds[c]--;

		for (var c = 'a'; c <= 'z'; c++)
		{
			if (atcoder.Contains(c))
			{
				if (ds[c] >= 0) tc -= ds[c];
				else sc += ds[c];
			}
			else
			{
				if (ds[c] != 0) return false;
			}
		}
		return sc >= 0 && tc >= 0;
	}
}
