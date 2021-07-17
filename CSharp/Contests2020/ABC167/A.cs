using System;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();
		Console.WriteLine(Console.ReadLine().StartsWith(s) ? "Yes" : "No");
	}
}
