using System;
using System.Linq;

class B
{
	static void Main()
	{
		var s = Console.ReadLine();
		Console.WriteLine(string.Join("", s.Reverse()) == s && s.Substring(0, s.Length / 2) == s.Substring((s.Length + 1) / 2) ? "Yes" : "No");
	}
}
