using System;

class C
{
	static void Main()
	{
		var x = int.Parse(Console.ReadLine());
		Console.WriteLine(x <= x / 100 * 105 ? 1 : 0);
	}
}
