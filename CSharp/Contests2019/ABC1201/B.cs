using System;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var x = Math.Ceiling(n / 1.08);
		Console.WriteLine((int)(1.08 * x) == n ? $"{x}" : ":(");
	}
}
