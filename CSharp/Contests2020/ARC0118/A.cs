using System;
using System.Linq;

class A
{
	static void Main()
	{
		var a = new int[3].Select(_ => int.Parse(Console.ReadLine())).ToArray();
		Console.WriteLine((a[2] - 1) / Math.Max(a[0], a[1]) + 1);
	}
}
