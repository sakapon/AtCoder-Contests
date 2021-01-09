using System;

class B
{
	static void Main()
	{
		var x = int.Parse(Console.ReadLine());
		Console.WriteLine(x / 500 * 1000 + x % 500 / 5 * 5);
	}
}
