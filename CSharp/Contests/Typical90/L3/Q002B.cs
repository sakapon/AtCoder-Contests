using System;

class Q002B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		for (int x = 0; x < 1 << n; x++)
		{
			var b = ToBrackets(x, n);
			if (IsRegularBracketSeq(b)) Console.WriteLine(b);
		}
	}

	static string ToBrackets(int x, int n)
	{
		var s = new char[n];
		for (int i = 0; i < n; i++)
			s[n - 1 - i] = (x & (1 << i)) == 0 ? '(' : ')';
		return new string(s);
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
