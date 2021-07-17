using System;

class Q002
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		if (n % 2 != 0) return;

		AllBoolCombination(n, b =>
		{
			var cs = Array.ConvertAll(b, f => f ? ')' : '(');
			Array.Reverse(cs);
			var s = new string(cs);
			if (IsRegularBracketSeq(s)) Console.WriteLine(s);
			return false;
		});
	}

	static void AllBoolCombination(int n, Func<bool[], bool> action)
	{
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;
		var b = new bool[n];

		for (int x = 0; x < pn; ++x)
		{
			for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;
			if (action(b)) break;
		}
	}

	static bool IsRegularBracketSeq(string s)
	{
		var t = 0;
		foreach (var c in s)
			if (c == '(') ++t;
			else if (c == ')') if (--t < 0) return false;
		return t == 0;
	}
}
