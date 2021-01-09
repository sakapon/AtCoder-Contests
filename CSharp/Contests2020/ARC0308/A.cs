using System;
using System.Linq;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();
		Console.WriteLine(string.Join("", Enumerable.Repeat("hi", s.Length / 2)) == s ? "Yes" : "No");
	}
}
