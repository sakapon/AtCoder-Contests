using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var ab = Read();
		var cd = Read();
		Console.WriteLine(ab[0] * cd[1] - ab[1] * cd[0]);
	}
}
