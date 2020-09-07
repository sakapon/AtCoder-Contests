using System;
using System.Linq;

class B2
{
	static void Main()
	{
		var s = Console.ReadLine();
		var h = s.Length / 2;
		Console.WriteLine(string.Join("", s.Reverse()) == s && s[..h] == s[^h..] ? "Yes" : "No");
	}
}
