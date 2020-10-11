using System;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();
		Console.WriteLine(s == "Y" ? t.ToUpper() : t);
	}
}
