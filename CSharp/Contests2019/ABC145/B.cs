using System;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		Console.WriteLine(s.Substring(0, n / 2) == s.Substring(n / 2) ? "Yes" : "No");
	}
}
