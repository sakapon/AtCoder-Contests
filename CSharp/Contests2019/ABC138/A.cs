using System;

class A
{
	static void Main()
	{
		var a = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		Console.WriteLine(a >= 3200 ? s : "red");
	}
}
