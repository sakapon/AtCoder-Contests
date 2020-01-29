using System;

class A
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine(500 * h[0] >= h[1] ? "Yes" : "No");
	}
}
