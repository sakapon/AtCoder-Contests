using System;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();
		var p = Console.ReadLine();
		int i = -1;
		while ((i = s.IndexOf(p, i + 1)) != -1) Console.WriteLine(i);
	}
}
