using System;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();
		Console.WriteLine(s == t ? "same" : s.Equals(t, StringComparison.InvariantCultureIgnoreCase) ? "case-insensitive" : "different");
	}
}
