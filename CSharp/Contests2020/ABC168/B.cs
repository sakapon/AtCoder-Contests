using System;

class B
{
	static void Main()
	{
		var k = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		Console.WriteLine(s.Length <= k ? s : s[..k] + "...");
	}
}
