using System;

namespace CoderLib6.Strings
{
	// Test: https://codeforces.com/contest/1304/problem/B
	static class StringHelper
	{
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
