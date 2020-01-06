using System;
using System.Linq;

class B
{
	static void Main()
	{
		var s = Console.ReadLine();
		Console.WriteLine(Enumerable.Range(0, s.Length / 2).Count(i => s[i] != s[s.Length - 1 - i]));
	}
}
