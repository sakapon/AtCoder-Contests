using System;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();
		Console.WriteLine(s[2] == s[3] && s[4] == s[5] ? "Yes" : "No");
	}
}
