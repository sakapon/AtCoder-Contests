using System;

class A0
{
	static void Main()
	{
		Console.ReadLine();
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Array.Sort(a);
		Console.WriteLine(string.Join(" ", a));
	}
}
