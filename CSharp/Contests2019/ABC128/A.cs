using System;

class A
{
	static void Main()
	{
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine((3 * a[0] + a[1]) / 2);
	}
}
