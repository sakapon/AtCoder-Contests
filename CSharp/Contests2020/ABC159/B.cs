using System;
using System.Linq;

class B
{
	static void Main()
	{
		var s = Console.ReadLine();
		Console.WriteLine(IsPalindrome(s) && IsPalindrome(s.Remove(s.Length / 2)) && IsPalindrome(s.Substring((s.Length + 1) / 2)) ? "Yes" : "No");
	}

	static bool IsPalindrome(string s) => Enumerable.Range(0, s.Length / 2).All(i => s[i] == s[s.Length - 1 - i]);
}
