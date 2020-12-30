using System;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();
		Console.WriteLine(s.Contains("ooo") ? "o" : s.Contains("xxx") ? "x" : "draw");
	}
}
