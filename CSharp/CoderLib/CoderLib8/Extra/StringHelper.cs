using System;

namespace CoderLib8.Extra
{
	// Test: https://codeforces.com/contest/1304/problem/B
	static class StringHelper
	{
		// '0' == 48
		static int FromNumberChar(this char c) => c - '0';
		static char ToNumberChar(this int i) => (char)(i + '0');

		// 'A' == 65
		static int FromUpperChar(this char c) => c - 'A';
		static char ToUpperChar(this int i) => (char)(i + 'A');

		// 'a' == 97
		static int FromLowerChar(this char c) => c - 'a';
		static char ToLowerChar(this int i) => (char)(i + 'a');

		static string Reverse(string s)
		{
			var c = s.ToCharArray();
			Array.Reverse(c);
			return new string(c);
		}

		static bool IsPalindrome(string s)
		{
			for (int i = 0; i < s.Length; ++i) if (s[i] != s[s.Length - 1 - i]) return false;
			return true;
		}

		// ()(), (()), (()()), etc.
		static bool IsRegularBracket(string s)
		{
			var t = 0;
			foreach (var c in s)
				if (c == '(') ++t;
				else if (c == ')') if (--t < 0) return false;
			return t == 0;
		}
	}
}
