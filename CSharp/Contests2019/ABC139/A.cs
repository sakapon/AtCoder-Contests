using System;
using System.Linq;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();
		Console.WriteLine(Enumerable.Range(0, s.Length).Count(i => s[i] == t[i]));
	}
}
