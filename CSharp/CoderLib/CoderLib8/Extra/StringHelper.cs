using System;

namespace CoderLib8.Extra
{
	// Test: https://codeforces.com/contest/1304/problem/B
	static class StringHelper
	{
		static int ToInt(this char c) => c - 48;
		static char ToNumber(this int i) => (char)(i + 48);

		static int ToIndexForUpper(this char c) => c - 65;
		static char ToUpper(this int i) => (char)(i + 65);

		static int ToIndexForLower(this char c) => c - 97;
		static char ToLower(this int i) => (char)(i + 97);

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
	}
}
