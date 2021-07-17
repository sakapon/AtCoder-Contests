using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var a = Read();
		Console.WriteLine(a[0] * a[1]);
	}
}
