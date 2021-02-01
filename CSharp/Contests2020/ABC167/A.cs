using System;

class A
{
	static void WriteYesNo(bool b) => Console.WriteLine(b ? "Yes" : "No");
	static void Main()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();
		WriteYesNo(t[0..s.Length] == s);
	}
}
