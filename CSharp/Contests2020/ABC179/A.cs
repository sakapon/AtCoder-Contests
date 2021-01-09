using System;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();
		Console.WriteLine(s + (s[^1] == 's' ? "es" : "s"));
	}
}
