using System;

class A
{
	static void Main()
	{
		var a = Console.ReadLine();
		var s = Console.ReadLine();
		Console.WriteLine(new string(Array.ConvertAll(s.ToCharArray(), c => char.IsNumber(c) ? a[c - '0'] : c)));
	}
}
