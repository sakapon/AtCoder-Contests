using System;

class C
{
	static decimal[] ReadDec() => Array.ConvertAll(Console.ReadLine().Split(), decimal.Parse);
	static void Main()
	{
		var a = ReadDec();
		Console.WriteLine(Math.Floor(a[0] * a[1]));
	}
}
