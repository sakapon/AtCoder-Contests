using System;
using System.Linq;

class B
{
	static void Main()
	{
		var s = Console.ReadLine();
		var s1 = s.Remove(s.Length / 2);
		var s2 = string.Join("", s1.Reverse());
		Console.WriteLine(s1 == s2 && $"{s1}{s[s.Length / 2]}{s1}" == s ? "Yes" : "No");
	}
}
